using Spire.Doc;
using TagsCloud.Core.Models;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;

namespace TagsCloud.Core.Services.WordsProcessing.WordsProviders;

public class DocWordsProvider : IWordsProvider
{
    private readonly IFileValidator fileValidator;

    public DocWordsProvider(IFileValidator fileValidator)
    {
        this.fileValidator = fileValidator;
    }

    public Result<List<string>> ReadFile(string path)
    {
        var validate = fileValidator.Validate(path, FileFormat);
        if (!validate.IsSuccess)
            return Result<List<string>>.Fail(validate.Error!);
        
        using var document = new Document();
        document.LoadFromFile(path);
        var text = document.GetText();

        var words = text
            .Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (words.Count == 0)
            return Result<List<string>>.Fail("Файл пуст");

        return Result<List<string>>.Ok(words);
    }

    public string FileFormat => "doc";
    public string Name => FileFormat;
}