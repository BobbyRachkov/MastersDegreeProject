using MastersProject.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastersProject.App.MathEngine
{
    internal interface IApproximationEngine
    {
        EquationDescriptor CalculateEquation(Point[] points);
    }
}
