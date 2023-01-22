using MastersProject.App.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class PfdWindowFactory : DefaultWindowFactory
    {
        public override Window Create()
        {
            var window = base.Create();
            window.Height = 600;
            window.Width = 600;
            window.Background = Brushes.Black;
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.ResizeMode = ResizeMode.NoResize;
            return window;
        }
    }
}
