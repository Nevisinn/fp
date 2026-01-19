using System.Drawing;
using TagsCloud.Core.Models;
using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Core.Services.WordsProcessing;
using TagsCloud.Core.Services.WordsProcessing.WordsPreprocessors;

namespace TagsCloud.Core.Services.ImageGeneration.CloudVisualizers;

public class FileCloudVisualizer : ICloudVisualizer
{
    private readonly IWordsPreprocessor wordsPreprocessor;

    public FileCloudVisualizer(IWordsPreprocessor wordsPreprocessor)
    {
        this.wordsPreprocessor = wordsPreprocessor;
    }

    public Result<string> VisualizeWordsWithOptions(List<string> words, ProgramOptions options)
    {
        var handleWords = wordsPreprocessor.Process(words, options);

        if (!handleWords.IsSuccess)
            return Result<string>.Fail(handleWords.Error!);

        var handledWords = handleWords.Value!;

        var wordsCounts = WordsCounter.GetWordsCounts(handledWords);

        var wordsSizes = WordsMeasurer.GetWordsSizes(wordsCounts, options.ImageOptions.Font);

        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;

        return CreateAndSave(wordsSizes, wordsCounts, projectDirectory, options);
    }

    private Result<string> CreateAndSave
    (
        Dictionary<string, Size> wordsSizes,
        Dictionary<string, int> wordsCounts,
        string currentDirectoryPath,
        ProgramOptions options)
    {
        var imageOptions = options.ImageOptions;
        var imageBounds = new RectangleF(
            -imageOptions.ImageSize.Width / 2f,
            -imageOptions.ImageSize.Height / 2f,
            imageOptions.ImageSize.Width,
            imageOptions.ImageSize.Height);
        var putRectangles = PutRectangles(wordsSizes.Values.ToList(), options.Algorithm, imageBounds);
        if (!putRectangles.IsSuccess)
            return Result<string>.Fail(putRectangles.Error!);

        var rectangles = putRectangles.Value!;
        var image = ImageCreator.CreateImageWithWordsLayout(wordsSizes, wordsCounts, rectangles, options.ImageOptions);
        var imageName = $"cloud_with_{rectangles.Count}_words";

        ImageSaver.Save($"{currentDirectoryPath}/Images/{imageName}.{options.ImageOptions.ImageFormat}", image);

        options.Algorithm.Reset();

        return Result<string>.Ok("Изображение успешно создано");
    }

    private Result<List<Rectangle>> PutRectangles(List<Size> wordsSizes, ICloudLayouter layouter,
        RectangleF imageBounds)
    {
        var result = new List<Rectangle>();
        foreach (var size in wordsSizes)
        {
            var putNextRect = layouter.PutNextRectangle(size);
            if (!putNextRect.IsSuccess)
                return Result<List<Rectangle>>.Fail(putNextRect.Error!);

            var rect = putNextRect.Value;
            if (!imageBounds.Contains(rect))
                return Result<List<Rectangle>>.Fail("Облако тегов не влезло в изображение заданного размера");

            result.Add(rect);
        }

        return Result<List<Rectangle>>.Ok(result);
    }
}