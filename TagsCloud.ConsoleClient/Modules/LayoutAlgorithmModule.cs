using Autofac;
using TagsCloud.Infrastructure.Selectors;
using TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Infrastructure.Services.LayoutAlgorithm.Spirals;

namespace TagsCloud.Modules;

public class LayoutAlgorithmModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();

        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();

        builder.RegisterType<AlgorithmSelector>().As<IAlgorithmSelector>();
    }
}