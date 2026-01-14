using TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;

namespace TagsCloud.Infrastructure.Selectors;

public class ColorSchemeSelector : BaseSelector<IColorSchemeProvider>, IColorSchemeSelector
{
    public ColorSchemeSelector(IEnumerable<IColorSchemeProvider> services) : base(services)
    {
    }
}