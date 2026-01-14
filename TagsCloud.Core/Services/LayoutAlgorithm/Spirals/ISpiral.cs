using System.Drawing;

namespace TagsCloud.Infrastructure.Services.LayoutAlgorithm.Spirals;

public interface ISpiral
{
    public Point GetNextPoint();
    public void Reset();
}