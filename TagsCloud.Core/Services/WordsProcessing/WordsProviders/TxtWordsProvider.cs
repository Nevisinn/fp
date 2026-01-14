using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

public class TxtWordsProvider : IWordsProvider
{
    private readonly IFileValidator fileValidator;

    public TxtWordsProvider(IFileValidator fileValidator)
    {
        this.fileValidator = fileValidator;
    }

    public List<string> ReadFile(string path)
    {
        fileValidator.Validate(path, FileFormat);

        var text = File.ReadAllText(path);
        var words = text
            .Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (words.Count == 0)
            throw new InvalidDataException("Файл пуст");

        return words;
    }

    public string FileFormat => "txt";
    public string Name => FileFormat;
}