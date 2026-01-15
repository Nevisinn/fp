using Autofac;
using TagsCloud.Core.Selectors;
using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Core.Services.LayoutAlgorithm.Spirals;

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