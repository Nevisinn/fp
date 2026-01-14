using System.Drawing;

namespace TagsCloud.Infrastructure.Services.WordsProcessing;

public static class WordsMeasurer
{
    public static Dictionary<string, Size> GetWordsSizes(Dictionary<string, int> wordsCounts, Font font)
    {
        var wordsSizes = new Dictionary<string, Size>();
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        var words = wordsCounts.Keys.ToList();

        foreach (var word in words)
        {
            var wordFontSize = font.Size + wordsCounts[word];
            var size = graphics.MeasureString(word, new Font(font.Name, wordFontSize)).ToSize();
            size.Width += 1;
            wordsSizes.Add(word, size);
        }

        return wordsSizes;
    }
}