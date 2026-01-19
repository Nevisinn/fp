using TagsCloud.Core.Models;
using TagsCloud.Core.Services.WordsProcessing.WordsHandlers;

namespace TagsCloud.Core.Services.WordsProcessing.WordsPreprocessors;

public class DefaultWordsPreprocessor : IWordsPreprocessor
{
    private readonly IEnumerable<IWordsHandler>? wordsHandlers;

    public DefaultWordsPreprocessor(IEnumerable<IWordsHandler> wordsHandlers)
    {
        this.wordsHandlers = wordsHandlers;
    }

    public Result<List<string>> Process(List<string> words, ProgramOptions options)
    {
        if (wordsHandlers == null)
            return Result<List<string>>.Fail("Обработчики не найдены");

        if (words.Any(word => word.Any(c => !char.IsLetter(c))))
            return Result<List<string>>.Fail(
                "Слова должны состоять только из букв и быть записаны по одному в каждой строке");

        var handlersList = wordsHandlers.ToList();

        foreach (var handler in handlersList)
            handler.Options = options;

        for (var i = 0; i < handlersList.Count - 1; i++)
            handlersList[i].NextHandler = handlersList[i + 1];

        var handleWords = handlersList.First().Handle(words);
        return !handleWords.IsSuccess
            ? Result<List<string>>.Fail(handleWords.Error!)
            : Result<List<string>>.Ok(handleWords.Value!.Select(w => w.ToLower()).ToList());
    }
}