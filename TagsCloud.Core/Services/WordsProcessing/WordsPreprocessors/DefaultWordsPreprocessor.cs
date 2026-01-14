using TagsCloud.Infrastructure.Models;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsHandlers;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsPreprocessors;

public class DefaultWordsPreprocessor : IWordsPreprocessor
{
    private readonly IEnumerable<IWordsHandler> wordsHandlers;

    public DefaultWordsPreprocessor(IEnumerable<IWordsHandler> wordsHandlers)
    {
        this.wordsHandlers = wordsHandlers;
    }

    public List<string> Process(List<string> words, ProgramOptions options)
    {
        if (wordsHandlers == null)
            throw new ArgumentNullException("Обработчики не найдены");

        if (words.Any(word => word.Any(c => !char.IsLetter(c))))
            throw new ArgumentException(
                "Слова должны состоять только из букв и быть записаны по одному в каждой строке");

        var handlersList = wordsHandlers.ToList();

        foreach (var handler in handlersList) handler.Options = options;

        for (var i = 0; i < handlersList.Count - 1; i++) handlersList[i].NextHandler = handlersList[i + 1];

        return handlersList.First().Handle(words).Select(w => w.ToLower()).ToList();
    }
}