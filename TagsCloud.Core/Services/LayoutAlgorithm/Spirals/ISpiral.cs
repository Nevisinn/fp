using System.Drawing;

namespace TagsCloud.Core.Services.LayoutAlgorithm.Spirals;

public interface ISpiral
{
    public Point GetNextPoint();
    public void Reset();
}