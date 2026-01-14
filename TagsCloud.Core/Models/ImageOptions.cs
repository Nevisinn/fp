using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

namespace TagsCloud.Infrastructure.Models;

public class ImageOptions
{
    public Color BackgroundColor { get; init; }
    public Size ImageSize { get; init; }
    public Color[] TextColors { get; init; }
    public required ImageFormat ImageFormat { get; init; }
    public required IColorSchemeProvider ColorScheme { get; init; }
    public required Font Font { get; init; }
}