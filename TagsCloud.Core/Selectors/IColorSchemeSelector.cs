using TagsCloud.Core.Models;
using TagsCloud.Core.Services.ImageGeneration.ColorSchemeProviders;

namespace TagsCloud.Core.Selectors;

public interface IColorSchemeSelector
{
    public Result<IColorSchemeProvider> Select(string name);
}