using TagsCloud.Core.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Core.Models;

public class ProgramOptions
{
    public required string InputWordsFilePath { get; init; }
    public required string InputBoringWordsFilePath { get; init; }
    public required ImageOptions ImageOptions { get; init; }
    public ICloudLayouter Algorithm { get; init; }
    public IWordsProvider WordsProvider { get; init; }
}