using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsHandlers;

public interface IWordsHandler
{
    public IWordsHandler? NextHandler { get; set; }
    public ProgramOptions Options { get; set; }
    public List<string> Handle(List<string> words);
}