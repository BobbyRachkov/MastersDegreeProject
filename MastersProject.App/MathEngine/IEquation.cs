namespace MastersProject.App.MathEngine;

internal interface IEquation
{
    double CalculateYValue(double targetXPoint);
    double CalculateXValue(double targetYPoint);
}