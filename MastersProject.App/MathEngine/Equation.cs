using MastersProject.App.Extensions;

namespace MastersProject.App.MathEngine
{
    internal sealed class Equation
    {
        public double SlopeCoefficient { get; }

        public double OffsetConstant { get; }

        public Equation(double slopeCoefficient, double offsetConstant)
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
    }
}
