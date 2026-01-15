using Autofac;
using TagsCloud.Core.Selectors;
using TagsCloud.Core.Services.ImageGeneration.CloudVisualizers;
using TagsCloud.Core.Services.ImageGeneration.ColorProvider;
using TagsCloud.Core.Services.ImageGeneration.ColorSchemeProviders;
using TagsCloud.Core.Services.ImageGeneration.FontProviders;

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