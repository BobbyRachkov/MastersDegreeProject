using System.Windows;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface IWindowFactory
    {
        Window Create();

        Window Create<TViewModel>(TViewModel viewModel);
    }
}
