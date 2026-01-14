using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.CloudVisualizers;

public interface ICloudVisualizer
{
    public void VisualizeWordsWithOptions(List<string> words, ProgramOptions options);
}