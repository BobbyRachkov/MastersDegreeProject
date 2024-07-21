using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class PfdWindowFactory : DynamicWindowFactory
    {
        public override Window Create()
        {
            var window = base.Create();
            window.Title = "PFD";
            window.Background = Brushes.Black;
            window.WindowStyle = WindowStyle.None;
            window.ResizeMode = ResizeMode.NoResize;
            return window;
        }
    }
}
