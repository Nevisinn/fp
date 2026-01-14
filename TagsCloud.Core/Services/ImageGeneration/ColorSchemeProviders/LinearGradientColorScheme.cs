using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

public class LinearGradientColorScheme : IColorSchemeProvider
{
    public Brush GetColorScheme(ImageOptions imageOptions)
    {
        if (imageOptions.TextColors.Length != 2)
            throw new ArgumentException("Для LinearGradient заливки нужно 2 цвета");

        var firstColor = imageOptions.TextColors[0];
        var secondColor = imageOptions.TextColors[1];
        var bounds = new Rectangle(0, 0, imageOptions.ImageSize.Width, imageOptions.ImageSize.Height);

        return new LinearGradientBrush(bounds, firstColor, secondColor, LinearGradientMode.Horizontal);
    }

    public string Name => "LinearGradient";
}