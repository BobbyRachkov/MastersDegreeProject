namespace MastersProject.App.Services.MathEngine
{
    internal interface IApproximationEngine
    {
        IEquation CalculateEquation(Point[] points, EquationOrder order);
    }
}
