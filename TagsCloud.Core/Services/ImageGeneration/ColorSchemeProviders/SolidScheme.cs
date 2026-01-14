using System.Drawing;
using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

public class SolidScheme : IColorSchemeProvider
{
    public Brush GetColorScheme(ImageOptions imageOptions)
    {
        if (imageOptions.TextColors.Length != 1)
            throw new ArgumentException("Для Solid заливки необходимо указать только 1 цвет");

        return new SolidBrush(imageOptions.TextColors[0]);
    }

    public string Name => "Solid";
}