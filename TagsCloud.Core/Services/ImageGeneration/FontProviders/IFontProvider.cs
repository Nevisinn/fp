using System.Drawing;

namespace TagsCloud.Core.Services.ImageGeneration.FontProviders;

public interface IFontProvider
{
    public Font GetFont(string fontName, float size);
}