namespace MastersProject.App.MathEngine
{
    public sealed class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static implicit operator Point((double x, double y) point)
        {
            return new Point(point.x, point.y);
        }
    }
}
