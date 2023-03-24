using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Models;
using MastersProject.SerialCommunicator;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Point = MastersProject.App.MathEngine.Point;

namespace MastersProject.App.ViewModels
{
    internal class PfdViewModel : ViewModelBase, IDisposable
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly IWindowManager _windowManager;

        public PfdViewModel( 
            SettingsViewModel settingsViewModel,
            IWindowManager windowManager, IAttitudeProvider attitudeIndicator)
        {
            _settingsViewModel = settingsViewModel;
            _windowManager = windowManager;
            AttitudeIndicator = attitudeIndicator;
            attitudeIndicator.ErrorOccurred += Serial_ErrorOccurred;

            OpenSettingsCommand = new RelayCommand((_) =>
            {
                _windowManager.ResetDefaultWindowFactory();
                _windowManager.ShowWindow(_settingsViewModel);
            });
            RestartCommand = new RelayCommand((_) =>
            {
                attitudeIndicator.RestartConnection();
            });
        }
        

        private void Serial_ErrorOccurred(object? sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }

        public IAttitudeProvider AttitudeIndicator { get; }
        public ICommand OpenSettingsCommand{ get; }
        public ICommand RestartCommand{ get; }

        public void Dispose()
        {
        }
    }
}
