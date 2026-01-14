using TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;

namespace TagsCloud.Infrastructure.Selectors;

public class AlgorithmSelector : BaseSelector<ICloudLayouter>, IAlgorithmSelector
{
    public AlgorithmSelector(IEnumerable<ICloudLayouter> services) : base(services)
    {
    }
}