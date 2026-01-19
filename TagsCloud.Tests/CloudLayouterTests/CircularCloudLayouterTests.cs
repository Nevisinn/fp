using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloud.Core.Extensions;
using TagsCloud.Core.Models;
using TagsCloud.Core.Services.ImageGeneration;
using TagsCloud.Core.Services.ImageGeneration.ColorSchemeProviders;
using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Core.Services.LayoutAlgorithm.Spirals;

namespace TagsCloud.Test.CloudLayouterTests;

[TestFixture]
public class CloudLayouterTests
{
    [TearDown]
    public void TearDown()
    {
        var currentContext = TestContext.CurrentContext;
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            return;

        var workingDirectory = currentContext.WorkDirectory;
        var currentProject = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        var filePath = Path.Combine(currentProject, "Fails", $"{currentContext.Test.Name}.png");
        var image = ImageCreator.CreateImageWithRectanglesLayout(cloudLayouter.Rectangles, imageOptions);
        ImageSaver.Save(filePath, image);

        TestContext.WriteLine($"Tag cloud visualization saved to file {filePath}");
        TestContext.AddTestAttachment(filePath);
    }

    private readonly CircularCloudLayouter cloudLayouter;
    private readonly ImageOptions imageOptions;
    private readonly Point center;

    public CloudLayouterTests()
    {
        center = new Point(0, 0);
        cloudLayouter = new CircularCloudLayouter(new ArchimedeanSpiral());
        imageOptions = new ImageOptions
        {
            BackgroundColor = Color.Black,
            ImageFormat = ImageFormat.Png,
            ColorScheme = new SolidScheme(),
            ImageSize = new Size(1000, 1000),
            Font = new Font("Arial", 14),
            TextColors = [Color.Red]
        };
    }

    [Test]
    public void PutNextRectangle_ShouldReturnFailure_WhenInvalidSize()
    {
        var createZeroSize = cloudLayouter.PutNextRectangle(new Size(0, 0));
        var createNegativeSize = cloudLayouter.PutNextRectangle(new Size(-10, -10));

        createZeroSize.IsSuccess.Should().BeFalse();
        createNegativeSize.IsSuccess.Should().BeFalse();
        createZeroSize.Error.Should().Be("Размер прямоугольника должен быть больше нуля");
        createNegativeSize.Error.Should().Be("Размер прямоугольника должен быть больше нуля");
    }

    [Test]
    public void PutNextRectangle_ShouldNotIntersects_WhenSameSize()
    {
        var putFirstRectangle = cloudLayouter.PutNextRectangle(new Size(10, 10));
        var putSecondRectangle = cloudLayouter.PutNextRectangle(new Size(10, 10));
        var rectangle1 = putFirstRectangle.Value;
        var rectangle2 = putSecondRectangle.Value;

        putFirstRectangle.IsSuccess.Should().BeTrue();
        putSecondRectangle.IsSuccess.Should().BeTrue();
        rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
    }

    [Test]
    public void PutNextRectangle_ShouldBeInCenter_WhenPutFirstRectangle()
    {
        var putRectangle = cloudLayouter.PutNextRectangle(new Size(10, 10));

        putRectangle.IsSuccess.Should().BeTrue();

        var rectangle = putRectangle.Value;
        var rectangleCenter = rectangle.Center();

        rectangleCenter.Should().Be(center);
    }

    [Test]
    public void PutNextRectangle_ShouldNotIntersects_WhenPutManyRectangles()
    {
        for (var i = 1; i < 100; i++) cloudLayouter.PutNextRectangle(new Size(i, i));

        foreach (var rectangle1 in cloudLayouter.Rectangles)
        foreach (var rectangle2 in cloudLayouter.Rectangles)
            if (rectangle1 != rectangle2)
                rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
    }

    [Test]
    public void PutNextRectangle_ShouldHaveCorrectSize_WhenPutRectangle()
    {
        var size = new Size(15, 20);

        var putRectangle = cloudLayouter.PutNextRectangle(size);
        putRectangle.IsSuccess.Should().BeTrue();

        var rectangle = putRectangle.Value;

        rectangle.Width.Should().Be(size.Width);
        rectangle.Height.Should().Be(size.Height);
    }
}