using System;
using System.Diagnostics;
using MastersProject.App.Extensions;
using Microsoft.VisualBasic.CompilerServices;

namespace MastersProject.App.Services.MathEngine
{
    [DebuggerDisplay("{SlopeCoefficient} ; {OffsetConstant}")]
    internal sealed class LinearEquation : IEquation
    {
        public double SlopeCoefficient { get; }

        public double OffsetConstant { get; }

        public LinearEquation(double slopeCoefficient, double offsetConstant)
        {
            SlopeCoefficient = slopeCoefficient;
            OffsetConstant = offsetConstant;
        }

        public double CalculateYValue(double targetXPoint)
        {
            return SlopeCoefficient * targetXPoint + OffsetConstant;
        }

        public double CalculateXValue(double targetYPoint)
        {
            SlopeCoefficient.AssertNotZero(nameof(SlopeCoefficient));
            return (targetYPoint - OffsetConstant) / SlopeCoefficient;
        }

        public static bool operator ==(LinearEquation lhs, LinearEquation rhs)
        {
            return Math.Abs(lhs.SlopeCoefficient - rhs.SlopeCoefficient) < 0.0000001
                && Math.Abs(lhs.OffsetConstant - rhs.OffsetConstant) < 0.0000001;
        }

        public static bool operator !=(LinearEquation lhs, LinearEquation rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not LinearEquation equation)
            {
                return false;
            }

            return ReferenceEquals(this, obj)
                   || this == equation;
        }

        public override int GetHashCode()
        {
            return (int)(SlopeCoefficient + OffsetConstant * 10000000);
        }
    }
}
