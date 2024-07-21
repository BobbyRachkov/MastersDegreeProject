namespace MastersProject.App.Models.CoordinateSystem
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
