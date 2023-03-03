using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using MastersProject.App.CoordinateSystem.Models;
using MastersProject.App.Extensions;
using MastersProject.App.Infrastructure;
using Point = MastersProject.App.MathEngine.Point;

namespace MastersProject.App.CoordinateSystem
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

        public double MaxPositiveValueX { get; set; }
        public double MaxPositiveValueY { get; set; }

        public double CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HalfHeight));
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
                RescaleX();
            }
        }

        public double HalfHeight => CanvasHeight / 2;
        public double HalfWidth => CanvasWidth / 2;

        private void RescaleX()
        {
            foreach (var point in Points)
            {
                point.MultiplierX = CanvasWidth / MaxPositiveValueX;
            }

            foreach (var line in Lines)
            {
                line.Point1.MultiplierX = CanvasWidth / MaxPositiveValueX;
                line.Point2.MultiplierX = CanvasWidth / MaxPositiveValueX;
            }
        }
        private void RescaleY()
        {
            foreach (var point in Points)
            {
                point.MultiplierY = CanvasHeight / MaxPositiveValueY;
            }

            foreach (var line in Lines)
            {
                line.Point1.MultiplierY = CanvasHeight / MaxPositiveValueY;
                line.Point2.MultiplierY = CanvasHeight / MaxPositiveValueY;
            }
        }
    }
}
