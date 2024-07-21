using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using MastersProject.App.CoordinateSystem;
using MastersProject.App.CoordinateSystem.Models;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.MathEngine;
using MastersProject.App.Models;
using MastersProject.App.UserControls;
using MastersProject.Serial;

namespace MastersProject.App.ViewModels
{
    internal class SettingsViewModel : PropertyChangedBase
    {
        private readonly ISerialCommunicator<SerialData> _serialCommunicator;
        private readonly ICollection<string> _serialPortNames;
        private DrawablePoint _currentPoint;

        public SettingsViewModel(
            ISerialCommunicator<SerialData> serialCommunicator,
            IApproximationEngine approximationEngine,
            IWindowManager windowManager,
            IAttitudeProvider attitudeProvider)
        {
            _serialPortNames = serialCommunicator.GetPortNames();
            _serialCommunicator = serialCommunicator;
            serialCommunicator.DataReceived += SerialCommunicator_DataReceived;

            DemoGraph = new CoordinateSystemViewModel(1023, 1023);
            PitchSetup = new(
                this,
                approximationEngine,
                windowManager,
                () => attitudeProvider.PitchEquation,
                e => attitudeProvider.PitchEquation = e)
            {
                Title = "Pitch"
            };
            RollSetup = new(
                this,
                approximationEngine,
                windowManager,
                () => attitudeProvider.RollEquation,
                e => attitudeProvider.RollEquation = e)
            {
                Title = "Roll"
            };

            _currentPoint = new DrawablePoint(0, 0, Brushes.Red);
            DemoGraph.Points.Add(_currentPoint);
            AddPointClick = new RelayCommand(AddPointToGraph);
        }

        private void SerialCommunicator_DataReceived(object? sender, SerialData e)
        {
            _currentPoint.X = e.Roll;
            _currentPoint.Y = e.Pitch;
            PitchSetup.AttitudeValue = e.Pitch;
            RollSetup.AttitudeValue = e.Roll;
        }

        private void AddPointToGraph()
        {
            var lockedPoint = _currentPoint;
            lockedPoint.Border = Brushes.MediumSlateBlue;
            lockedPoint.Fill = Brushes.MediumSlateBlue;
            _currentPoint = new(
                lockedPoint.X,
                lockedPoint.Y,
                lockedPoint.MultiplierX,
                lockedPoint.MultiplierY,
                Brushes.Red);

            DemoGraph.Points.Add(_currentPoint);

            var line = DemoGraph.Lines.LastOrDefault();
            if (line is null)
            {
                line = new DrawableLine(lockedPoint, _currentPoint, Brushes.Blue, 1);
                DemoGraph.Lines.Add(line);
                return;
            }

            if (!ReferenceEquals(line.Point2, lockedPoint))
            {
                line = new DrawableLine(lockedPoint, _currentPoint, Brushes.Blue, 1);
                DemoGraph.Lines.Add(line);
            }
        }

        public ICollection<string> SerialPortNames => _serialPortNames;

        public RelayCommand AddPointClick { get; }

        public CoordinateSystemViewModel DemoGraph { get; }
        public MathSetupPaneViewModel PitchSetup { get; }
        public MathSetupPaneViewModel RollSetup { get; }
    }
}
