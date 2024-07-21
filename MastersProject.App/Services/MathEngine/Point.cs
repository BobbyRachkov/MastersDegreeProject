namespace MastersProject.App.Services.MathEngine
{
    public sealed record Point(double X, double Y)
    {
        public double X { get; } = X;
        public double Y { get; } = Y;


        public static implicit operator Point((double x, double y) point)
        {
            return new Point(point.x, point.y);
        }
    }
}
