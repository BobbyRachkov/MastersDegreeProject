using Autofac;
using JetBrains.Annotations;
using MastersProject.App.Extensions;

namespace MastersProject.App
{
    [UsedImplicitly]
    internal sealed class DependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterWindowManager();

            builder.RegisterInfrastructure();

            builder.RegisterSerialCommunicator();

            builder.RegisterViewModels();
        }
    }
}
