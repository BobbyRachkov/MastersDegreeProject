using System;
using System.Collections.Generic;
using System.Windows.Threading;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.MathEngine;
using MastersProject.App.Models;
using MastersProject.SerialCommunicator;

namespace MastersProject.App.Infrastructure
{
    internal sealed class AttitudeProvider : PropertyChangedBase, IAttitudeProvider, IDisposable
    {
        private readonly ISerialCommunicator<SerialData> _serial;
        private readonly List<Exception> _errors;
        private Equation _pitchEquation;
        private Equation _rollEquation;
        private int _rawPitch;
        private int _rawRoll;

        public AttitudeProvider(IApproximationEngine approximationEngine, ISerialCommunicator<SerialData> serial)
        {
            _serial = serial;
            _errors = new();
            _pitchEquation = new Equation(0, 0);
            _rollEquation = new Equation(0, 0);

            Point[] points = new[]
            {
                new Point(1023, 60),
                new Point(0,-60)
            };
            var eqn = approximationEngine.CalculateEquation(points);

            PitchEquation = eqn;
            RollEquation = eqn;


            _serial.TrySetup("COM8", 9600);
            _serial.DataReceived += Serial_DataReceived;
            _serial.StartAsync();

            _serial.ErrorOccurred += Serial_ErrorOccurred;

        }

        private void Serial_ErrorOccurred(object? sender, Exception e)
        {
            _errors.Add(e);
            ErrorOccurred?.Invoke(this, e);
        }

        private void Serial_DataReceived(object? sender, SerialData e)
        {
            UpdateRawValues(e);
        }

        public IReadOnlyList<Exception> Errors => _errors;
        public event EventHandler<Exception>? ErrorOccurred;

        public double Pitch => PitchEquation.CalculateYValue(RawPitch);

        public double Roll => RollEquation.CalculateYValue(RawRoll);

        public int RawPitch
        {
            get => _rawPitch;
            set
            {
                _rawPitch = value;
                NotifyPropertyChanged(nameof(RawPitch));
                NotifyPropertyChanged(nameof(Pitch));
            }
        }

        public int RawRoll
        {
            get => _rawRoll;
            set
            {
                _rawRoll = value;
                NotifyPropertyChanged(nameof(RawRoll));
                NotifyPropertyChanged(nameof(Roll));
            }
        }

        public long Timestamp { get; set; }

        public int Index { get; set; }

        public Equation PitchEquation
        {
            get => _pitchEquation;
            set
            {
                _pitchEquation = value;
                NotifyPropertyChanged(nameof(PitchEquation));
                NotifyPropertyChanged(nameof(Pitch));
            }
        }

        public Equation RollEquation
        {
            get => _rollEquation;
            set
            {
                _rollEquation = value;
                NotifyPropertyChanged(nameof(RollEquation));
                NotifyPropertyChanged(nameof(Roll));
            }
        }

        private void UpdateRawValues(SerialData newData)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                RawPitch = newData.Pitch;
                RawRoll = newData.Roll;
                Timestamp = newData.Timestamp;
                Index = newData.Index;
            });
        }

        public void RestartConnection()
        {
            _serial.Restart();
        }

        public override string ToString()
        {
            return $"Index: {Index:00000}; Pitch: {RawPitch:0000}; Roll: {RawRoll:0000}; Timestamp: {Timestamp}";
        }

        public void Dispose()
        {
            _serial.Stop();
            _serial.DataReceived -= Serial_DataReceived;
            _serial.ErrorOccurred -= Serial_ErrorOccurred;
        }
    }
}
