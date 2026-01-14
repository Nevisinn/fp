namespace TagsCloud.Infrastructure.Services.WordsProcessing;

public static class WordsCounter
{
    public static Dictionary<string, int> GetWordsCounts(List<string> words)
    {
        var wordsCounts = new Dictionary<string, int>();

        foreach (var word in words)
            if (!wordsCounts.TryAdd(word, 0))
                wordsCounts[word]++;

        return wordsCounts;
    }
}