using TagsCloud.Infrastructure.Models;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsPreprocessors;

public interface IWordsPreprocessor
{
    public List<string> Process(List<string> words, ProgramOptions options);
}