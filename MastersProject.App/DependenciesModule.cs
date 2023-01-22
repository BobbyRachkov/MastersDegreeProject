using Autofac;
using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.Models;
using MastersProject.App.Translators;
using MastersProject.App.ViewModels;
using MastersProject.SerialCommunicator;

namespace MastersProject.App
{
    internal sealed class DependenciesModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultWindowFactory>()
                .AsImplementedInterfaces();
            builder.RegisterType<PfdWindowFactory>()
                .AsImplementedInterfaces();

            builder.RegisterType<WindowManager>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<DefaultTranslator>()
                .AsImplementedInterfaces();

            builder.RegisterType<SerialPortCommunicator<AttitudeInformation>>()
                .As<ISerialCommunicator<AttitudeInformation>>()
                .SingleInstance();

            builder.RegisterType<MainViewModel>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
