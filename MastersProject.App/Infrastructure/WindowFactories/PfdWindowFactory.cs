using System.Windows;
using System.Windows.Media;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class PfdWindowFactory : DefaultWindowFactory
    {
        public override Window Create()
        {
            var window = base.Create();
            window.Title = "PFD";
            window.Height = 768;
            window.Width = 768;
            window.Background = Brushes.Black;
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.ResizeMode = ResizeMode.NoResize;
            return window;
        }
    }
}
