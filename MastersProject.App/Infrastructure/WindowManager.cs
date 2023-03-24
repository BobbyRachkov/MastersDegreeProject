using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MastersProject.App.Infrastructure.WindowManager;
using System.Windows;
using MastersProject.App.Infrastructure.Interfaces;
using Autofac;

namespace MastersProject.App.Infrastructure
{
    internal class WindowManager : IWindowManager
    {
        private readonly List<Window> _windows;
        private readonly ILifetimeScope _serviceProvider;
        private readonly List<IWindowFactory> _windowFactories;

        public IWindowFactory ActiveWindowFactory { get; private set; }

        public IReadOnlyCollection<Window> Windows => _windows;
        public WindowManager(ILifetimeScope serviceProvider, IEnumerable<IWindowFactory> windowFactories)
        {
            _serviceProvider = serviceProvider;
            _windows = new();
            _windowFactories = windowFactories.ToList();
            ActiveWindowFactory = _windowFactories.First();
        }
        public Window ShowWindow<TViewModel>() where TViewModel : class
        {
            var viewModel = _serviceProvider.Resolve<TViewModel>();
            return ShowWindow(viewModel);
        }
        public Window ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            var window = ActiveWindowFactory.Create(viewModel);
            BindClosable(window, viewModel);
            _windows.Add(window);
            window.Show();
            return window;
        }
        public bool? ShowDialog<TViewModel, TViewModelOwner>(TViewModelOwner owner)
            where TViewModel : class
            where TViewModelOwner : class
        {
            var viewModel = _serviceProvider.Resolve<TViewModel>();
            return ShowDialog(viewModel, owner);
        }
        public bool? ShowDialog<TViewModel, TViewModelOwner>(TViewModel viewModel, TViewModelOwner owner)
            where TViewModel : class
            where TViewModelOwner : class
        {
            var window = ActiveWindowFactory.Create(viewModel);
            window.Owner = _windows.Single(x => (x.Content as TViewModelOwner) == owner);
            BindClosable(window, viewModel);
            _windows.Add(window);
            return window.ShowDialog();
        }
        public void CloseWindow<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            var window = _windows.FirstOrDefault(x => x.Content == viewModel);
            CloseWindow(window);
        }
        public void CloseWindow(Window? window)
        {
            if (window == null)
                return;
            _windows.Remove(window);
            window?.Close();
        }

        public void ResetDefaultWindowFactory()
        {
            ActiveWindowFactory = _windowFactories.First();
        }
        public void SetActiveFactory<TFactory>() where TFactory : class, IWindowFactory
        {
            ActiveWindowFactory = _windowFactories.Single(x => x.GetType() == typeof(TFactory));
        }
        public bool TryAddFactory(IWindowFactory factory)
        {
            if (_windowFactories.Any(x => x.GetType() == factory.GetType()))
            {
                return false;
            }
            _windowFactories.Add(factory);
            return true;
        }

        private void BindClosable<TViewModel>(Window window, TViewModel viewModel)
        {
            window.Closed += (sender, _) => CloseWindow(sender as Window);
            if (viewModel is ICanClose canCloseVm)
            {
                canCloseVm.Close += window.Close;
                window.Closed += (sender, _) => canCloseVm.OnClose((sender as Window)?.DialogResult);
            }
            if (viewModel is ICanCancelClose canCancelCloseVm)
            {
                window.Closing += (_, args) => canCancelCloseVm.OnClosing(args);
            }
            if (viewModel is ICanHideShow canHideShowVm)
            {
                canHideShowVm.Show += window.Show;
                canHideShowVm.Hide += window.Hide;
            }
        }
    }
}
