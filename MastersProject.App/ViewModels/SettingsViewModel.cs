using MastersProject.App.Infrastructure;
using MastersProject.App.Models;
using MastersProject.SerialCommunicator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MastersProject.App.MathEngine;
using Point = MastersProject.App.MathEngine.Point;
using MastersProject.App.CoordinateSystem;
using MastersProject.App.CoordinateSystem.Models;
using MastersProject.App.UserControls;

namespace MastersProject.App.ViewModels
{
    internal class SettingsViewModel : PropertyChangedBase
    {
        private readonly ISerialCommunicator<SerialData> _serialCommunicator;
        private readonly ICollection<string> _serialPortNames;
        private string? _selectedSerialPort;
        private double _xCoordinate;
        private double _yCoordinate;
        private DrawablePoint _currentPoint;

        public SettingsViewModel(ISerialCommunicator<SerialData> serialCommunicator)
        {
            _serialPortNames = serialCommunicator.GetPortNames();
            _serialCommunicator = serialCommunicator;
            _selectedSerialPort = _serialPortNames.LastOrDefault();
            serialCommunicator.DataReceived += SerialCommunicator_DataReceived;

            DemoGraph = new CoordinateSystemViewModel(1023, 1023);
            PitchSetup = new()
            {
                Title = "Pitch"
            };
            RollSetup= new()
            {
                Title = "Roll"
            };

            _currentPoint = new DrawablePoint(0, 0, Brushes.Red);
            DemoGraph.Points.Add(_currentPoint);
            AddPointClick = new RelayCommand((_) => AddPointToGraph());
        }

        private void SerialCommunicator_DataReceived(object? sender, SerialData e)
        {
            _currentPoint.X = e.Roll;
            _currentPoint.Y = e.Pitch;
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

            if(!ReferenceEquals(line.Point2, lockedPoint))
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
