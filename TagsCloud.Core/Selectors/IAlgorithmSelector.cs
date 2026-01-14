using TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;

namespace TagsCloud.Infrastructure.Selectors;

public interface IAlgorithmSelector
{
    public ICloudLayouter Select(string name);
}