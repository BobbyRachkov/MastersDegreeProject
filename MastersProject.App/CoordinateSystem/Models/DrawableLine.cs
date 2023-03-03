using System.Windows.Media;
using MastersProject.App.Infrastructure;

namespace MastersProject.App.CoordinateSystem.Models;

internal class DrawableLine : PropertyChangedBase
{
    private DrawablePoint _point1;
    private DrawablePoint _point2;
    private Brush _fill;
    private double _thickness;


    public DrawableLine(DrawablePoint point1, DrawablePoint point2)
    {
        _point1 = point1;
        _point2 = point2;
        _fill = Brushes.Black;
        _thickness = 1;
    }

    public DrawableLine(DrawablePoint point1, DrawablePoint point2, Brush fill, double thickness) : this(point1, point2)
    {
        _fill = fill;
        _thickness = thickness;
    }

    public DrawablePoint Point1
    {
        get => _point1;
        set
        {
            _point1 = value;
            NotifyPropertyChanged();
        }
    }

    public DrawablePoint Point2
    {
        get => _point2;
        set
        {
            _point2 = value;
            NotifyPropertyChanged();
        }
    }

    public Brush? Stroke
    {
        get => _fill;
        set
        {
            _fill = value;
            NotifyPropertyChanged();
        }
    }

    public double Thickness
    {
        get => _thickness;
        set
        {
            _thickness = value;
            NotifyPropertyChanged();
        }
    }
}