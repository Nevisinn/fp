using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.ColorSchemeProviders;

public interface IColorSchemeProvider : INamedService
{
    public Brush GetColorScheme(ImageOptions imageOptions);
}