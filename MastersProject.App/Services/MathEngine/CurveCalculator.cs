using System;
using System.Linq;
using MathNet.Numerics;

namespace MastersProject.App.Services.MathEngine
{
    public sealed class CurveCalculator : IApproximationEngine
    {
        IEquation IApproximationEngine.CalculateEquation(Point[] points, EquationOrder order)
        {
            return order switch
            {
                EquationOrder.Linear => CalculateLinear(points),
                _ => throw new ArgumentOutOfRangeException($"Equation order {order} is not supported.")
            };
        }

        private IEquation CalculateLinear(Point[] points)
        {
            var x = points.Select(p => p.X).ToArray();
            var y = points.Select(p => p.Y).ToArray();
            var equationMembers = Fit.Line(x, y);
            return new LinearEquation(equationMembers.B, equationMembers.A);
        }
    }
}
