using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;

namespace TagsCloud.Infrastructure.Selectors;

public class DocumentWriterSelector : BaseSelector<IDocumentWriter>, IDocumentWriterSelector
{
    public DocumentWriterSelector(IEnumerable<IDocumentWriter> services) : base(services)
    {
    }
}