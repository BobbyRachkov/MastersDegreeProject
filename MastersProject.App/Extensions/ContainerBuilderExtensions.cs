using System.Reflection;
using Autofac;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.ViewModels.PortSelector;
using MastersProject.App.ViewModels;
using MastersProject.App.Infrastructure;
using MastersProject.App.Models;
using MastersProject.App.Translators;
using MastersProject.Serial;
using MastersProject.Serial.SerialWrapper;

namespace MastersProject.App.Extensions;

public static class ContainerBuilderExtensions
{
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

    public static void RegisterWindowFactories(this ContainerBuilder builder)
    {
        builder.RegisterType<DefaultWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<DynamicWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<PfdWindowFactory>()
            .AsImplementedInterfaces();
        builder.RegisterType<DotSelectorFactory>()
            .AsImplementedInterfaces();

        builder.RegisterType<WindowManager>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    public static void RegisterSerialCommunicator(this ContainerBuilder builder)
    {
        builder.RegisterType<DefaultTranslator>()
            .AsImplementedInterfaces();

        builder.RegisterType<SerialWrapper>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<SerialPortCommunicator<SerialData>>()
            .As<ISerialCommunicator<SerialData>>()
            .SingleInstance();
    }

    public static void RegisterMockSerialCommunicator(this ContainerBuilder builder)
    {
        builder.RegisterType<DefaultTranslator>()
            .AsImplementedInterfaces();

        builder.RegisterType<MockWrapper>()
            .AsImplementedInterfaces()
            .SingleInstance();

        builder.RegisterType<SerialPortCommunicator<SerialData>>()
            .As<ISerialCommunicator<SerialData>>()
            .SingleInstance();
    }
}