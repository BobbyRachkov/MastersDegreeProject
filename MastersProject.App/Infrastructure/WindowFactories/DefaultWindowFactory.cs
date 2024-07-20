using System.Windows;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.WindowBases;

namespace MastersProject.App.Infrastructure.WindowFactories
{
    internal class DefaultWindowFactory : IWindowFactory
    {
        public virtual Window Create()
        {
            var window = new DefaultWindow
            {
                Height = 600,
                Width = 900
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
