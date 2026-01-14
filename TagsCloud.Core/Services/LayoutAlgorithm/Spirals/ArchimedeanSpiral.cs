using System.Drawing;

namespace TagsCloud.Infrastructure.Services.LayoutAlgorithm.Spirals;

public class ArchimedeanSpiral : ISpiral
{
    private const float AngleStep = 0.1f;
    private const float SpiralStep = 0.5f;
    private double angle;
    public Point Center { get; init; }

    public Point GetNextPoint()
    {
        var radius = SpiralStep * angle;
        var x = Center.X + (int)(radius * Math.Cos(angle));
        var y = Center.Y + (int)(radius * Math.Sin(angle));
        angle += AngleStep;

        return new Point(x, y);
    }

    public void Reset()
    {
        angle = 0;
    }
}