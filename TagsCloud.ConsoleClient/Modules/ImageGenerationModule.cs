using Autofac;
using TagsCloud.Infrastructure.Selectors;
using TagsCloud.Infrastructure.Services.ImageGeneration.CloudVisualizers;
using TagsCloud.Infrastructure.Services.ImageGeneration.ColorProvider;
using TagsCloud.Infrastructure.Services.ImageGeneration.ColorSchemeProviders;
using TagsCloud.Infrastructure.Services.ImageGeneration.FontProviders;

namespace TagsCloud.Modules;

public class ImageGenerationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ColorProvider>().As<IColorProvider>();

        builder.RegisterType<SolidScheme>().As<IColorSchemeProvider>();
        builder.RegisterType<LinearGradientColorScheme>().As<IColorSchemeProvider>();

        builder.RegisterType<ColorSchemeSelector>().As<IColorSchemeSelector>();

        builder.RegisterType<FileCloudVisualizer>().As<ICloudVisualizer>();

        builder.RegisterType<SystemFontProvider>().As<IFontProvider>();
    }
}