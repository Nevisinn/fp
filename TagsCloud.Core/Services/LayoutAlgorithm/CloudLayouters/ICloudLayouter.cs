using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;

public interface ICloudLayouter : INamedService
{
    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
    public void Reset();
}