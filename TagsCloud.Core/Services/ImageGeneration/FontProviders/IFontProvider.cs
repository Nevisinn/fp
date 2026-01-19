using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.FontProviders;

public interface IFontProvider
{
    public Result<Font> GetFont(string fontName, float size);
}