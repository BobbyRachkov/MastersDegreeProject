using System.Windows;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.WindowBases;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class DynamicWindowFactory : IWindowFactory
    {
        public virtual Window Create()
        {
            var window = new DefaultWindow
            {
                Height = double.NaN,
                Width = double.NaN,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            return window;
        }

        public virtual Window Create<TViewModel>(TViewModel viewModel)
        {
            var window = Create();
            window.Content = viewModel;
            return window;
        }
    }
}
