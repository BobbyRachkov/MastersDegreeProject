using System;
using System.Linq;
using System.Windows.Media;
using MastersProject.App.Infrastructure.Interfaces;
using MastersProject.App.Infrastructure.Mvvm;
using MastersProject.App.Infrastructure.WindowFactories;
using MastersProject.App.Models.CoordinateSystem;
using MastersProject.App.Services.MathEngine;

namespace MastersProject.App.ViewModels;

internal class MathSetupPaneViewModel : PropertyChangedBase
{
    private readonly SettingsViewModel _settingsViewModel;
    private readonly IApproximationEngine _approximationEngine;
    private readonly IWindowManager _windowManager;
    private readonly Func<IEquation> _currentEquationSource;
    private IEquation _equation;
    private readonly Action<IEquation> _applyEquationCallback;
    private IEquation? _previousEquation;
    private double _attitudeValue;

    public MathSetupPaneViewModel(
        SettingsViewModel settingsViewModel,
        IApproximationEngine approximationEngine,
        IWindowManager windowManager,
        Func<IEquation> currentEquationSource,
        Action<IEquation> applyEquationCallback)
    {
        _settingsViewModel = settingsViewModel;
        _approximationEngine = approximationEngine;
        _applyEquationCallback = applyEquationCallback;
        _windowManager = windowManager;
        _currentEquationSource = currentEquationSource;
        _equation = ActiveEquation;

        Graph = new(1023, 180);
        var selectorLine = new DrawableLine(
            new DrawablePoint(0, -10, Brushes.White),
            new DrawablePoint(0, Graph.MaxPositiveValueY + 20, Brushes.White),
            Brushes.Red, 0.5);
        Graph.Lines.Add(selectorLine);

        ClearLinesCommand = new(
            () => Graph.Lines.RemoveAt(1),
            () => Graph.Lines.Count == 2);
        ClearDotsCommand = new(Graph.Points.Clear, Graph.Points.Any);
        CalculateTrendLineCommand = new(
            CalculateTrendLine,
            () => Graph.Points.Count > 1 && Graph.Lines.Count == 1);
        PickDotCommand = new(PickDot);
        UseEquationCommand = new(UseEquation, () => Equation != ActiveEquation);
        RevertEquationCommand = new(RevertEquation, () => _previousEquation is not null);
    }

    public string? Title { get; init; }

    public double AttitudeValue
    {
        get => _attitudeValue;
        set
        {
            _attitudeValue = value;
            NotifyPropertyChanged();
            var selectorLine = Graph.Lines.First();
            selectorLine.Point1.X = value;
            selectorLine.Point2.X = value;
        }
    }

    public IEquation Equation
    {
        get => _equation;
        set
        {
            _equation = value;
            NotifyPropertyChanged();
        }
    }
    public IEquation ActiveEquation
    {
        get => _currentEquationSource();
        set
        {
            _applyEquationCallback(value);
            NotifyPropertyChanged();
        }
    }

    public CoordinateSystemViewModel Graph { get; }
    public RelayCommand ClearLinesCommand { get; set; }
    public RelayCommand ClearDotsCommand { get; set; }
    public RelayCommand CalculateTrendLineCommand { get; set; }
    public RelayCommand PickDotCommand { get; set; }
    public RelayCommand UseEquationCommand { get; set; }
    public RelayCommand RevertEquationCommand { get; set; }

    private void CalculateTrendLine()
    {
        var points =
            Graph
            .Points
            .Select(p => new Point(p.X, p.Y))
            .ToArray();
        Equation = _approximationEngine.CalculateEquation(points, EquationOrder.Linear);

        var p1 = new DrawablePoint(
            0,
            Equation.CalculateYValue(0),
            Graph.CurrentMultiplierX,
            Graph.CurrentMultiplierY,
            Brushes.Black)
        {
            TransformY = new(-1, Graph.MaxPositiveValueY / 2)
        };
        var p2 = new DrawablePoint(
            Graph.MaxPositiveValueX,
            Equation.CalculateYValue(Graph.MaxPositiveValueX),
            Graph.CurrentMultiplierX,
            Graph.CurrentMultiplierY,

            Brushes.Black)
        {
            TransformY = new(-1, Graph.MaxPositiveValueY / 2)
        };

        var line = new DrawableLine(p1, p2, Brushes.Black, 2);
        Graph.Lines.Add(line);
    }
    private void PickDot()
    {
        var dotPicker = new DotSelectorViewModel(AttitudeValue, AddNewPoint);

        _windowManager.SetActiveFactory<DotSelectorFactory>();
        _windowManager.ShowDialog(dotPicker, _settingsViewModel);
    }

    private void AddNewPoint(double x, double y)
    {
        var point = new DrawablePoint(x, y,
            Graph.CurrentMultiplierX,
            Graph.CurrentMultiplierY,
            Brushes.Red)
        {
            TransformY = new(-1, Graph.MaxPositiveValueY / 2)
        };
        Graph.Points.Add(point);
    }

    private void UseEquation()
    {
        _previousEquation = ActiveEquation;
        ActiveEquation = Equation;
    }

    private void RevertEquation()
    {
        if (_previousEquation is null)
        {
            return;
        }
        var previousEquation = _previousEquation;
        Equation = previousEquation;
        UseEquation();
    }
}