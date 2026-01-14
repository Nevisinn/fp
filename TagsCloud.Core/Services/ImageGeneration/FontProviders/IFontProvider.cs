using System.Drawing;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.FontProviders;

public interface IFontProvider
{
    public Font GetFont(string fontName, float size);
}