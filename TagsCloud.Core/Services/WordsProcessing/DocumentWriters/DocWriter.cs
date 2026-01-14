using Spire.Doc;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

public class DocWriter : IDocumentWriter
{
    public void WriteText(string path, string text)
    {
        using var document = new Document();
        var section = document.AddSection();
        var paragraph = section.AddParagraph();
        paragraph.AppendText(text);
        document.SaveToFile(path, FileFormat.Doc);
    }

    public string Name => "doc";
}