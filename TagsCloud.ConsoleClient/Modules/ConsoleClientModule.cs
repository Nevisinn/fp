using Autofac;

namespace TagsCloud.Modules;

public class ConsoleClientModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProgramOptionsMapper>().SingleInstance();
        builder.RegisterType<ConsoleUi>().SingleInstance();
    }
}