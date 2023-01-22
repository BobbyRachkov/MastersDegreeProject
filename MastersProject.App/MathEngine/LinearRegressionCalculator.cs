using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.App.MathEngine
{
    public sealed class LinearRegressionCalculator : IApproximationEngine
    {
        EquationDescriptor IApproximationEngine.CalculateEquation(Point[] points)
        {
            var x = points.Select(p => p.X).ToArray();
            var y = points.Select(p => p.Y).ToArray();
            var equationMembers = Fit.Line(x, y);
            return new EquationDescriptor(equationMembers.B, equationMembers.A);
        }
    }
}
