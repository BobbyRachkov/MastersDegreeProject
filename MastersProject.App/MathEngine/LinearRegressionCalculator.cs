using System.Linq;
using MathNet.Numerics;

namespace MastersProject.App.MathEngine
{
    public sealed class LinearRegressionCalculator : IApproximationEngine
    {
        Equation IApproximationEngine.CalculateEquation(Point[] points)
        {
            var x = points.Select(p => p.X).ToArray();
            var y = points.Select(p => p.Y).ToArray();
            var equationMembers = Fit.Line(x, y);
            return new Equation(equationMembers.B, equationMembers.A);
        }
    }
}
