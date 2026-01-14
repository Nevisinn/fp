using Spire.Doc;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

public class DocxWriter : IDocumentWriter
{
    public void WriteText(string path, string text)
    {
        using var document = new Document();
        var section = document.AddSection();
        var paragraph = section.AddParagraph();
        paragraph.AppendText(text);
        document.SaveToFile(path, FileFormat.Docx);
    }

    public string Name => "docx";
}