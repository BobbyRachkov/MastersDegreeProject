using System;
using System.Windows;
using System.Windows.Input;
using MastersProject.App.Infrastructure.DataStreaming;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.Models.DataStreaming;
using MastersProject.App.Services.AttitudeProvider;

namespace MastersProject.App.ViewModels
{
    internal class PfdViewModel : ViewModelBase, IDisposable
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly IWindowManager _windowManager;
        private readonly IDataStreamReader<AttitudeDataFrame> _attitudeDataReader;
        private int _rawPitch;
        private int _rawRoll;
        private double _pitch;
        private double _roll;

        public PfdViewModel(
            SettingsViewModel settingsViewModel,
            IWindowManager windowManager,
            IAttitudeProvider attitudeIndicator,
            IDataStreamReader<AttitudeDataFrame> attitudeDataReader)
        {
            _settingsViewModel = settingsViewModel;
            _windowManager = windowManager;
            _attitudeDataReader = attitudeDataReader;
            AttitudeIndicator = attitudeIndicator;
            attitudeIndicator.ErrorOccurred += Serial_ErrorOccurred;

            OpenSettingsCommand = new RelayCommand((_) =>
            {
                _windowManager.SetActiveFactory<SettingsWindowFactory>();
                _windowManager.ShowWindow(_settingsViewModel);
            });
            RestartCommand = new RelayCommand((_) =>
            {
                attitudeIndicator.RestartConnection();
             });

            _attitudeDataReader.NewEntryPublished += OnNewAttitudeEntryPublished;
        }

        private void OnNewAttitudeEntryPublished(object? sender, AttitudeDataFrame e)
        {
            OnUiThread(() =>
            {
                Pitch = e.Pitch;
                Roll = e.Roll;
                RawPitch = e.RawPitch;
                RawRoll = e.RawRoll;
            });
        }

        private void Serial_ErrorOccurred(object? sender, Exception e)
        {
            MessageBox.Show(e.Message);
        }

        public double Pitch
        {
            get => _pitch;
            set
            {
                _pitch = value;
                NotifyPropertyChanged();
            }
        }

        public double Roll
        {
            get => _roll;
            set
            {
                _roll = value;
                NotifyPropertyChanged();
            }
        }

        public int RawPitch
        {
            get => _rawPitch;
            set
            {
                _rawPitch = value;
                NotifyPropertyChanged();
            }
        }

        public int RawRoll
        {
            get => _rawRoll;
            set
            {
                _rawRoll = value;
                NotifyPropertyChanged();
            }
        }


        public IAttitudeProvider AttitudeIndicator { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand RestartCommand { get; }

        public void Dispose()
        {
        }
    }
}
