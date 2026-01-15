using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.WordsProcessing.WordsProviders;

public interface IWordsProvider : INamedService
{
    public string FileFormat { get; }
    public Result<List<string>> ReadFile(string path);
}