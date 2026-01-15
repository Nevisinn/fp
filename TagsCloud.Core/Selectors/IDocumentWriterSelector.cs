using TagsCloud.Core.Models;
using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;

namespace TagsCloud.Core.Selectors;

public interface IDocumentWriterSelector
{
    public Result<IDocumentWriter> Select(string name);
}