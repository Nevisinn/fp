using Spire.Doc;
using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

public class DocxWordsProvider : IWordsProvider
{
    private readonly IFileValidator fileValidator;

    public DocxWordsProvider(IFileValidator fileValidator)
    {
        this.fileValidator = fileValidator;
    }

    public List<string> ReadFile(string path)
    {
        fileValidator.Validate(path, FileFormat);

        using var document = new Document();
        document.LoadFromFile(path);
        var text = document.GetText();
        var words = text
            .Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        if (words.Count == 0)
            throw new InvalidDataException("Файл пуст");

        return words;
    }

    public string FileFormat => "docx";
    public string Name => FileFormat;
}