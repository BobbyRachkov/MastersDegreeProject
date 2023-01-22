using System.Collections.Generic;
using System.Windows;

namespace MastersProject.App.Infrastructure.Interfaces
{
    internal interface IWindowManager
    {
        IReadOnlyCollection<Window> Windows { get; }
        IWindowFactory ActiveWindowFactory { get; }
        Window ShowWindow<TViewModel>() where TViewModel : class;
        Window ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : class;

        bool? ShowDialog<TViewModel, TViewModelOwner>(TViewModelOwner owner)
            where TViewModel : class
            where TViewModelOwner : class;

        bool? ShowDialog<TViewModel, TViewModelOwner>(TViewModel viewModel, TViewModelOwner owner)
            where TViewModel : class
            where TViewModelOwner : class;
        void CloseWindow<TViewModel>(TViewModel viewModel) where TViewModel : class;
        void CloseWindow(Window window);
        void ResetDefaultWindowFactory();

        void SetActiveFactory<TFactory>() where TFactory : class, IWindowFactory;
        bool TryAddFactory(IWindowFactory factory);
    }
}
