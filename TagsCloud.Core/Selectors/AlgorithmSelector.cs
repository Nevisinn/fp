using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;

namespace TagsCloud.Core.Selectors;

public class AlgorithmSelector : BaseSelector<ICloudLayouter>, IAlgorithmSelector
{
    public AlgorithmSelector(IEnumerable<ICloudLayouter> services) : base(services)
    {
    }
}