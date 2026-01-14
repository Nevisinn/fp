using System.Drawing;
using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

public interface IColorSchemeProvider : INamedService
{
    public Brush GetColorScheme(ImageOptions imageOptions);
}