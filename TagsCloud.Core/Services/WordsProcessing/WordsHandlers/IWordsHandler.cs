using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.WordsProcessing.WordsHandlers;

public interface IWordsHandler
{
    public IWordsHandler? NextHandler { get; set; }
    public ProgramOptions Options { get; set; }
    public Result<List<string>> Handle(List<string> words);
}