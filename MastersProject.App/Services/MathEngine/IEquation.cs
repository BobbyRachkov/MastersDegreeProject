namespace MastersProject.App.Services.MathEngine;

internal interface IEquation
{
    double CalculateYValue(double targetXPoint);
    double CalculateXValue(double targetYPoint);
}