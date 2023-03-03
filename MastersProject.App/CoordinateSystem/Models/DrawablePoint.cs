using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MastersProject.App.Extensions;
using MastersProject.App.Infrastructure;

namespace MastersProject.App.CoordinateSystem.Models
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    internal class DrawablePoint : PropertyChangedBase
    {
        private double _x;
        private double _y;
        private double _multiplierX;
        private double _multiplierY;
        private Brush _border;
        private Brush _fill;

        public DrawablePoint(double x, double y) : this(x, y, 1, 1)
        {

        }

        public DrawablePoint(double x, double y, Brush fillAndBorder) : this(x, y, 1, 1, fillAndBorder, fillAndBorder)
        {

        }

        public DrawablePoint(double x, double y, double multiplierX, double multiplierY, Brush fillAndBorder)
            : this(x, y, multiplierX, multiplierY, fillAndBorder, fillAndBorder)
        {
        }

        public DrawablePoint(double x, double y, double multiplierX, double multiplierY)
            : this(x, y, multiplierX, multiplierY, Brushes.Black, Brushes.Black)
        {
        }

        public DrawablePoint(double x, double y, double multiplierX, double multiplierY, Brush fill, Brush border)
        {
            _x = x;
            _y = y;
            _multiplierX = multiplierX;
            _multiplierY = multiplierY;
            _fill = fill;
            _border = border;
        }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ComputedX));
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ComputedY));
            }
        }

        public double ComputedX
        {
            get => X * MultiplierX;
            set => X = value / MultiplierX;
        }

        public double ComputedY
        {
            get => Y * MultiplierY;
            set => Y = value / MultiplierY;
        }

        public double MultiplierX
        {
            get => _multiplierX;
            set
            {
                value.AssertNotZero(nameof(MultiplierX));
                _multiplierX = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ComputedX));
            }
        }

        public double MultiplierY
        {
            get => _multiplierY;
            set
            {
                value.AssertNotZero(nameof(MultiplierY));
                _multiplierY = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ComputedY));
            }
        }

        public Brush Fill
        {
            get => _fill;
            set
            {
                _fill = value;
                NotifyPropertyChanged();
            }
        }

        public Brush Border
        {
            get => _border;
            set
            {
                _border = value;
                NotifyPropertyChanged();
            }
        }
    }
}
