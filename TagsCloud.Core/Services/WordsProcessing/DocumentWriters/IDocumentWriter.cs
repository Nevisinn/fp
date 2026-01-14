namespace TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

public interface IDocumentWriter : INamedService
{
    public void WriteText(string path, string text);
}