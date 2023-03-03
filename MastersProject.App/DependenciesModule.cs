using Autofac;
using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.MathEngine;
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

            builder.RegisterType<SerialPortCommunicator<SerialData>>()
                .As<ISerialCommunicator<SerialData>>()
                .SingleInstance();

            builder.RegisterType<SettingsViewModel>()
                .AsSelf()
                .SingleInstance(); 
            builder.RegisterType<PfdViewModel>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<LinearRegressionCalculator>()
                .As<IApproximationEngine>();
        }
    }
}
