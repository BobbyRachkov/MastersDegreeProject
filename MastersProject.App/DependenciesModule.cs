using Autofac;
using MastersProject.App.Models;
using MastersProject.App.Translators;
using MastersProject.SerialCommunicator;

namespace MastersProject.App
{
    internal sealed class DependenciesModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultTranslator>()
                .AsImplementedInterfaces();

            builder.RegisterType<SerialPortCommunicator<AttitudeInformation>>()
                .As<ISerialCommunicator<AttitudeInformation>>()
                .SingleInstance();
        }
    }
}
