using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Core.Selectors;

public class WordsProviderSelector : BaseSelector<IWordsProvider>, IWordsProviderSelector
{
    public WordsProviderSelector(IEnumerable<IWordsProvider> services) : base(services)
    {
    }
}