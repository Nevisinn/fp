using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;

namespace TagsCloud.Core.Selectors;

public class DocumentWriterSelector : BaseSelector<IDocumentWriter>, IDocumentWriterSelector
{
    public DocumentWriterSelector(IEnumerable<IDocumentWriter> services) : base(services)
    {
    }
}