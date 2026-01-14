using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

namespace TagsCloud.Infrastructure.Selectors;

public interface IDocumentWriterSelector
{
    public IDocumentWriter Select(string name);
}