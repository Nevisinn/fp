using System.Drawing;

namespace TagsCloud.Core.Services.ImageGeneration.FontProviders;

public class SystemFontProvider : IFontProvider
{
    public Font GetFont(string fontName, float size)
    {
        return new Font(fontName, size);
    }
}