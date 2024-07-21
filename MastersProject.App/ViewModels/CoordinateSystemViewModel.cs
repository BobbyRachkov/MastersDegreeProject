using System.Collections.ObjectModel;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Models.CoordinateSystem;

namespace MastersProject.App.ViewModels
{
    internal class CoordinateSystemViewModel : PropertyChangedBase
    {
        private double _canvasHeight;
        private double _canvasWidth;

        public CoordinateSystemViewModel(double maxPositiveValueX, double maxPositiveValueY)
        {
            MaxPositiveValueX = maxPositiveValueX;
            MaxPositiveValueY = maxPositiveValueY;
            Points = new();
            Lines = new();
        }


        public ObservableCollection<DrawablePoint> Points { get; }
        public ObservableCollection<DrawableLine> Lines { get; }

        public double MaxPositiveValueX { get; init; }
        public double MaxPositiveValueY { get; init; }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HalfHeight));
                NotifyPropertyChanged(nameof(CurrentMultiplierY));
                RescaleY();
            }
        }
        public double CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                _canvasWidth = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HalfWidth));
                NotifyPropertyChanged(nameof(CurrentMultiplierX));
                RescaleX();
            }
        }

        public double HalfHeight => CanvasHeight / 2;
        public double HalfWidth => CanvasWidth / 2;

        public double CurrentMultiplierX => CanvasWidth / MaxPositiveValueX;
        public double CurrentMultiplierY => CanvasHeight / MaxPositiveValueY;

        private void RescaleX()
        {
            foreach (var point in Points)
            {
                point.MultiplierX = CurrentMultiplierX;
            }

            foreach (var line in Lines)
            {
                line.Point1.MultiplierX = CurrentMultiplierX;
                line.Point2.MultiplierX = CurrentMultiplierX;
            }
        }
        private void RescaleY()
        {
            foreach (var point in Points)
            {
                point.MultiplierY = CurrentMultiplierY;
            }

            foreach (var line in Lines)
            {
                line.Point1.MultiplierY = CurrentMultiplierY;
                line.Point2.MultiplierY = CurrentMultiplierY;
            }
        }
    }
}
