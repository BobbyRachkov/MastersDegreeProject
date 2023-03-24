using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastersProject.App.Infrastructure;

namespace MastersProject.App.CoordinateSystem.Models
{
    internal class Transform
    {
        public Transform()
        {
            Scale = 1;
            Offset = 0;
        }

        public Transform(double scale, double offset)
        {
            Scale = scale;
            Offset = offset;
        }

        public double Scale { get; init; }
        public double Offset { get; init; }

        public double Calculate(double value)
        {
            return value * Scale + Offset;
        }
    }
}
