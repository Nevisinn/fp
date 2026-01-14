using System.Drawing;
using System.Drawing.Imaging;
using FakeItEasy;
using FluentAssertions;
using TagsCloud.Infrastructure.Models;
using TagsCloud.Infrastructure.Services.ImageGeneration.CloudVisualizers;
using TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;
using TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsPreprocessors;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.ImageGenerationTests.CloudVisualizerTests;

[TestFixture]
public class FileCloudVisualizerTests
{
    [TearDown]
    public void TearDown()
    {
        if (File.Exists(outputImageFilePath))
            File.Delete(outputImageFilePath);
    }

    private readonly FileCloudVisualizer visualizer;
    private readonly ProgramOptions options;
    private readonly string outputImageFilePath;

    private readonly List<string> words =
    [
        "Привет",
        "тест",
        "Контур",
        "Школа"
    ];

    public FileCloudVisualizerTests()
    {
        var wordsPreprocessor = A.Fake<IWordsPreprocessor>();
        var layouter = A.Fake<ICloudLayouter>();
        options = new ProgramOptions
        {
            Algorithm = layouter,
            InputWordsFilePath = "",
            InputBoringWordsFilePath = "",
            ImageOptions = new ImageOptions
            {
                BackgroundColor = Color.Black,
                ColorScheme = new SolidScheme(),
                Font = new Font("Arial", 12),
                ImageSize = new Size(1000, 1000),
                TextColors = [Color.Indigo],
                ImageFormat = ImageFormat.Png
            },
            WordsProvider = A.Fake<IWordsProvider>()
        };
        visualizer = new FileCloudVisualizer(wordsPreprocessor);
        var preProcessedWords = new List<string> { "привет", "контур", "школа" };
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        outputImageFilePath = @$"{projectDirectory}\Images\cloud_with_{preProcessedWords.Count}_words" +
                              $".{options.ImageOptions.ImageFormat}";
        A.CallTo(() => layouter.PutNextRectangle(A<Size>._))
            .Returns(new Rectangle(0, 0, 10, 10));
        A.CallTo(() => wordsPreprocessor.Process(words, options))
            .Returns(preProcessedWords);
    }

    [Test]
    public void Visualize_ShouldCreateFile()
    {
        visualizer.VisualizeWordsWithOptions(words, options);

        File.Exists(outputImageFilePath).Should().BeTrue();
    }

    [Test]
    public void Visualize_ShouldCreateFileWithCorrectExtension()
    {
        visualizer.VisualizeWordsWithOptions(words, options);

        File.Exists(outputImageFilePath).Should().BeTrue();
        Path.GetExtension(outputImageFilePath)
            .Should()
            .Be("." + options.ImageOptions.ImageFormat);
    }

    [Test]
    public void Visualize_ShouldCreateNonEmptyFile()
    {
        visualizer.VisualizeWordsWithOptions(words, options);

        var fileInfo = new FileInfo(outputImageFilePath);

        fileInfo.Exists.Should().BeTrue();
        fileInfo.Length.Should().BeGreaterThan(0);
    }

    [Test]
    public void Visualize_ShouldCreateImageWithCorrectResolution()
    {
        visualizer.VisualizeWordsWithOptions(words, options);

        using var bitmap = new Bitmap(outputImageFilePath);

        bitmap.Width.Should().Be(options.ImageOptions.ImageSize.Width);
        bitmap.Height.Should().Be(options.ImageOptions.ImageSize.Height);
    }
}