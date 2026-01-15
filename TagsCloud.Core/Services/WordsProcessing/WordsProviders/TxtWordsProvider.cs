using TagsCloud.Core.Models;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;

namespace TagsCloud.Core.Services.WordsProcessing.WordsProviders;

public class TxtWordsProvider : IWordsProvider
{
    private readonly IFileValidator fileValidator;

    public TxtWordsProvider(IFileValidator fileValidator)
    {
        this.fileValidator = fileValidator;
    }

    public Result<List<string>> ReadFile(string path)
    {
        var validate = fileValidator.Validate(path, FileFormat);
        if (!validate.IsSuccess)
            return Result<List<string>>.Fail(validate.Error!);
        
        var text = File.ReadAllText(path);
        var words = text
            .Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (words.Count == 0)
            Result<List<string>>.Fail("Файл пуст");

        return Result<List<string>>.Ok(words);
    }

    public string FileFormat => "txt";
    public string Name => FileFormat;
}