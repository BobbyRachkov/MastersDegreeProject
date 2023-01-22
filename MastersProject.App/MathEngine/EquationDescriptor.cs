namespace MastersProject.App.MathEngine
{
    internal sealed class EquationDescriptor
    {
        public double SlopeCoefficient { get; }

        public double OffsetConstant { get; }

        public EquationDescriptor(double slopeCoefficient, double offsetConstant)
        {
            SlopeCoefficient = slopeCoefficient;
            OffsetConstant = offsetConstant;
        }

        public double CalculateValue(double targetPoint)
        {
            return SlopeCoefficient * targetPoint + OffsetConstant;
        }
    }
}
