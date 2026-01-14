namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

public interface IWordsProvider : INamedService
{
    public string FileFormat { get; }
    public List<string> ReadFile(string path);
}