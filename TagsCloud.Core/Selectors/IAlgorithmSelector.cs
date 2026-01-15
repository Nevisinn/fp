using TagsCloud.Core.Models;
using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;

namespace TagsCloud.Core.Selectors;

public interface IAlgorithmSelector
{
    public Result<ICloudLayouter> Select(string name);
}