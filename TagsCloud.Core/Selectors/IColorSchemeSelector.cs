using TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

namespace TagsCloud.Infrastructure.Selectors;

public interface IColorSchemeSelector
{
    public IColorSchemeProvider Select(string name);
}