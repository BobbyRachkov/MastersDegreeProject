using Autofac;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.ViewModels.PortSelector;
using MastersProject.App.ViewModels;
using MastersProject.App.Infrastructure;
using MastersProject.App.Models.SerialCommunication;
using MastersProject.App.Services.AttitudeDataStream;
using MastersProject.App.Services.EquationManager;
using MastersProject.App.Services.InputStream;
using MastersProject.App.Translators;
using MastersProject.Serial;
using MastersProject.Serial.SerialWrapper;
using MastersProject.App.Services.AttitudeProvider;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.Extensions;

public static class ContainerBuilderExtensions
{
    public static void RegisterInfrastructure(this ContainerBuilder builder)
    {
        builder.RegisterType<AttitudeDataStreamService>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<EquationManager>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<CurveCalculator>()
            .As<IApproximationEngine>();

        builder.RegisterType<AttitudeProvider>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    public static void RegisterViewModels(this ContainerBuilder builder)
    {
        builder.RegisterType<SettingsViewModel>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<PfdViewModel>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<PortSelectorViewModel>()
            .AsSelf()
            .SingleInstance();
    }

    public static void RegisterWindowManager(this ContainerBuilder builder)
    {
        builder.RegisterType<DefaultWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<DynamicWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<PfdWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<DotSelectorFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<SettingsWindowFactory>()
            .AsImplementedInterfaces();

        builder.RegisterType<WindowManager>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    public static void RegisterSerialCommunicator(this ContainerBuilder builder)
    {
        builder.RegisterType<SerialWrapper>()
            .AsImplementedInterfaces()
            .SingleInstance();

        RegisterCommonSerialServices(builder);
    }


    public static void RegisterMockSerialCommunicator(this ContainerBuilder builder)
    {
        builder.RegisterType<MockWrapper>()
            .AsImplementedInterfaces()
            .SingleInstance();

        RegisterCommonSerialServices(builder);
    }

    private static void RegisterCommonSerialServices(ContainerBuilder builder)
    {
        builder.RegisterType<SerialManager>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<DefaultTranslator>()
            .AsImplementedInterfaces();

        builder.RegisterType<SerialPortCommunicator<SerialData>>()
            .As<ISerialCommunicator<SerialData>>()
            .SingleInstance();
    }
}