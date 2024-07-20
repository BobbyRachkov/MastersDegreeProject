using System.Windows;

namespace MastersProject.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppBootstrapper.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            AppBootstrapper.OnShutdown();
        }
    }
}
