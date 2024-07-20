namespace MastersProject.App.MathEngine
{
    internal interface IApproximationEngine
    {
        IEquation CalculateEquation(Point[] points, EquationOrder order);
    }
}
