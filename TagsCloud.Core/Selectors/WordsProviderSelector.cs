using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Infrastructure.Selectors;

public class WordsProviderSelector : BaseSelector<IWordsProvider>, IWordsProviderSelector
{
    public WordsProviderSelector(IEnumerable<IWordsProvider> services) : base(services)
    {
    }
}