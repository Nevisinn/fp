using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.WordsProcessing.WordsPreprocessors;

public interface IWordsPreprocessor
{
    public Result<List<string>> Process(List<string> words, ProgramOptions options);

}