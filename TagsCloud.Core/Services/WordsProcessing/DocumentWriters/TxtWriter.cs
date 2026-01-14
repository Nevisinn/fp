namespace TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

public class TxtWriter : IDocumentWriter
{
    public void WriteText(string path, string text)
    {
        File.WriteAllText(path, text);
    }

    public string Name => "txt";
}