using TagsCloud.Core.Services.ImageGeneration.ColorSchemeProviders;

namespace TagsCloud.Core.Selectors;

public class ColorSchemeSelector : BaseSelector<IColorSchemeProvider>, IColorSchemeSelector
{
    public ColorSchemeSelector(IEnumerable<IColorSchemeProvider> services) : base(services)
    {
    }
}