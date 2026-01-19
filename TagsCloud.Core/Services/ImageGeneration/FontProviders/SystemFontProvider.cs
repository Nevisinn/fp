using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.FontProviders;

public class SystemFontProvider : IFontProvider
{
    public Result<Font> GetFont(string fontName, float size)
    {
        if (size <= 0)
            return Result<Font>.Fail("Размер шрифта должен быть больше нуля");

        var font = new Font(fontName, size);

        return font.Name != fontName
            ? Result<Font>.Fail($"Шрифт {fontName} не найден")
            : Result<Font>.Ok(font);
    }
}