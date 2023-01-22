using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MastersProject.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container = null!;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules();
            _container = builder.Build(); 
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _container?.Dispose();
        }
    }
}
