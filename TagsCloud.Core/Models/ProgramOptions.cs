using TagsCloud.Infrastructure.Services.LayoutAlgorithm.CloudLayouters;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Infrastructure.Models;

public class ProgramOptions
{
    public required string InputWordsFilePath { get; init; }
    public required string InputBoringWordsFilePath { get; init; }
    public required ImageOptions ImageOptions { get; init; }
    public ICloudLayouter Algorithm { get; init; }
    public IWordsProvider WordsProvider { get; init; }
}