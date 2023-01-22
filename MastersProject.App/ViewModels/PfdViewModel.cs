using MastersProject.App.Infrastructure;
using MastersProject.App.MathEngine;
using MastersProject.App.Models;
using MastersProject.SerialCommunicator;
using System;
using System.Windows.Markup;

namespace MastersProject.App.ViewModels
{
    internal class PfdViewModel : ViewModelBase, IDisposable
    {
        public readonly ISerialCommunicator<AttitudeInformation> _serial;
        private readonly IApproximationEngine _approximationEngine;

        public PfdViewModel(ISerialCommunicator<AttitudeInformation> serial, IApproximationEngine approximationEngine)
        {
            _serial = serial;
            _approximationEngine = approximationEngine;
            AttitudeIndicator = new();

            Point[] points = new[] 
            {
                new Point(1023.0, 60.0),
                new Point(0.0,-60.0)
            };
            var eqn = _approximationEngine.CalculateEquation(points);

            AttitudeIndicator.PitchEquation = eqn;
            AttitudeIndicator.RollEquation = eqn;


            _serial.Setup("COM8", 9600);
            _serial.StartAsync((data) =>
            {
                OnUiThread(() =>
                {
                    AttitudeIndicator.RawPitch = data.RawPitch;
                    AttitudeIndicator.RawRoll = data.RawRoll;
                });
            });
        }

        public AttitudeInformation AttitudeIndicator { get; set; }

        public void Dispose()
        {
            _serial.StopAsync();
        }
    }
}
