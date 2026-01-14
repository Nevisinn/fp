using System.Drawing;
using System.Numerics;
using TagsCloud.Infrastructure.Extensions;
using TagsCloud.Infrastructure.Services.LayoutAlgorithm.Spirals;

namespace TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly Point center;
    private readonly ISpiral spiral;

    public CircularCloudLayouter(ISpiral spiral)
    {
        center = new Point(0, 0);
        this.spiral = spiral;
    }

    public List<Rectangle> Rectangles { get; } = [];

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Размер прямоугольника должен быть больше нуля");

        var rect = PlaceNext(rectangleSize);
        rect = MoveCloserToCenter(rect);
        Rectangles.Add(rect);

        return rect;
    }

    public void Reset()
    {
        Rectangles.Clear();
        spiral.Reset();
    }

    public string Name => "Circular";

    private Rectangle PlaceNext(Size size)
    {
        while (true)
        {
            var point = spiral.GetNextPoint();
            var rect = new Rectangle(point.X - size.Width / 2, point.Y - size.Height / 2, size.Width, size.Height);
            if (!Rectangles.Any(r => r.IntersectsWith(rect)))
                return rect;
        }
    }

    private Rectangle MoveCloserToCenter(Rectangle rectangle)
    {
        var moved = rectangle;
        var centerPosition = new Vector2(center.X, center.Y);
        const float stepSize = 0.5f;

        while (true)
        {
            var rectCenter = rectangle.Center();
            var rectangleCenterPosition = new Vector2(rectCenter.X, rectCenter.Y);
            var direction = centerPosition - rectangleCenterPosition;
            if (direction.LengthSquared() == 0)
                break;

            var unitDirection = Vector2.Normalize(direction);
            var offset = unitDirection * stepSize;
            var testRect = moved with { X = (int)(moved.X + offset.X), Y = (int)(moved.Y + offset.Y) };
            if (Rectangles.Any(r => r.IntersectsWith(testRect)))
                break;

            moved = testRect;
        }

        return moved;
    }
}