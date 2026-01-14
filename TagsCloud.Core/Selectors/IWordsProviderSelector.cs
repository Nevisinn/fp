using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Infrastructure.Selectors;

public interface IWordsProviderSelector
{
    public IWordsProvider Select(string name);
}