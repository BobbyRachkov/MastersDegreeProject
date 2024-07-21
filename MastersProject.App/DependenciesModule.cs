using Autofac;
using JetBrains.Annotations;
using MastersProject.App.Extensions;
using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.Models;
using MastersProject.App.Services.AttitudeProvider;
using MastersProject.App.Services.MathEngine;
using MastersProject.App.Translators;
using MastersProject.App.ViewModels;
using MastersProject.App.ViewModels.PortSelector;
using MastersProject.Serial;
using MastersProject.Serial.SerialWrapper;

namespace MastersProject.App
{
    [UsedImplicitly]
    internal sealed class DependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterWindowManager();

            builder.RegisterSerialCommunicator();

            builder.RegisterViewModels();


            builder.RegisterType<CurveCalculator>()
                .As<IApproximationEngine>();

            builder.RegisterType<AttitudeProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
