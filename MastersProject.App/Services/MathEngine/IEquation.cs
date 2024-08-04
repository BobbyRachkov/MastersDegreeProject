namespace MastersProject.App.Services.MathEngine;

public interface IEquation
{
    double CalculateYValue(double targetXPoint);
    double CalculateXValue(double targetYPoint);
}