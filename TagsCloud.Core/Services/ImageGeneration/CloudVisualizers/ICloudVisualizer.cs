using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.CloudVisualizers;

public interface ICloudVisualizer
{
    public Result<string> VisualizeWordsWithOptions(List<string> words, ProgramOptions options);
}