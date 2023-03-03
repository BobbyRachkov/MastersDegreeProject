using MastersProject.App.Infrastructure;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.MathEngine;
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
        public readonly ISerialCommunicator<SerialData> _serial;
        private readonly IApproximationEngine _approximationEngine;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly IWindowManager _windowManager;

        public PfdViewModel(
            ISerialCommunicator<SerialData> serial,
            IApproximationEngine approximationEngine, 
            SettingsViewModel settingsViewModel,
            IWindowManager windowManager)
        {
            _serial = serial;
            _approximationEngine = approximationEngine;
            _settingsViewModel = settingsViewModel;
            _windowManager = windowManager;
            AttitudeIndicator = new();

            Point[] points = new[] 
            {
                new Point(1023f, 60f),
                new Point(0f,-60.0)
            };
            var eqn = _approximationEngine.CalculateEquation(points);

            AttitudeIndicator.PitchEquation = eqn;
            AttitudeIndicator.RollEquation = eqn;


            _serial.TrySetup("COM8", 9600);
            _serial.DataReceived += Serial_DataReceived;
            _serial.StartAsync();

            _serial.ErrorOccured += Serial_ErrorOccured;

            OpenSettingsCommand = new RelayCommand((_) =>
            {
                _windowManager.ResetDefaultWindowFactory();
                _windowManager.ShowWindow(_settingsViewModel);
            });
            RestartCommand = new RelayCommand((_) =>
            {
                _serial.Restart();
            });
        }

        private void Serial_DataReceived(object? sender, SerialData data)
        {
            OnUiThread(() =>
            {
                AttitudeIndicator.UpdateRawValues(data);
            });
        }

        private void Serial_ErrorOccured(object? sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }

        public AttitudeInformation AttitudeIndicator { get; }
        public ICommand OpenSettingsCommand{ get; }
        public ICommand RestartCommand{ get; }

        public void Dispose()
        {
            _serial.Stop();
        }
    }
}
