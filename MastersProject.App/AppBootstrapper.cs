using Autofac;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.ViewModels;
using MastersProject.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MastersProject.App
{
    internal static class AppBootstrapper
    {
        private static IContainer _container = null!;
        public static void OnStartup(StartupEventArgs _)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            _container = builder.Build();

            var windowManager = _container.Resolve<IWindowManager>();
            windowManager.SetActiveFactory<PfdWindowFactory>();
            windowManager.ShowWindow<MainViewModel>();
        }
        public static void OnShutdown()
        {
            _container?.Dispose();
        }
    }
}
