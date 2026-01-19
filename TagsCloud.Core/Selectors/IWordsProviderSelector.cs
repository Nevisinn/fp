using TagsCloud.Core.Models;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Core.Selectors;

public interface IWordsProviderSelector
{
    public Result<IWordsProvider> Select(string name, string parameterName);
}