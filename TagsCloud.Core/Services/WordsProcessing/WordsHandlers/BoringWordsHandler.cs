using TagsCloud.Core.Models;
using TagsCloud.Core.Selectors;

namespace TagsCloud.Core.Services.WordsProcessing.WordsHandlers;

public class BoringWordsHandler : IWordsHandler
{
    private readonly IWordsProviderSelector selector;

    public BoringWordsHandler(IWordsProviderSelector selector)
    {
        this.selector = selector;
    }

    public IWordsHandler? NextHandler { get; set; }
    public ProgramOptions Options { get; set; } = null!;

    public Result<List<string>> Handle(List<string> words)
    {
        var boringWordsFileExtension = Path.GetExtension(Options.InputBoringWordsFilePath).TrimStart('.').ToLower();
        var selectProvider = selector
            .Select(boringWordsFileExtension, nameof(Options.InputBoringWordsFilePath));

        if (!selectProvider.IsSuccess)
            return Result<List<string>>.Fail(selectProvider.Error!);

        var provider = selectProvider.Value!;

        var readFile = provider.ReadFile(Options.InputBoringWordsFilePath);
        if (!readFile.IsSuccess)
            return Result<List<string>>.Fail(readFile.Error!);

        var boringWords = readFile.Value!;

        var validateBoringWords = ValidateBoringWords(boringWords);
        if (!validateBoringWords.IsSuccess)
            return Result<List<string>>.Fail(validateBoringWords.Error!);

        var handledWords = new List<string>();

        foreach (var word in words)
            if (!boringWords.Contains(word))
                handledWords.Add(word);

        return NextHandler != null ? NextHandler.Handle(handledWords) : Result<List<string>>.Ok(handledWords);
    }

    private Result<None> ValidateBoringWords(List<string>? boringWords)
    {
        if (boringWords == null) return Result<None>.Fail("Список скучных слов не может быть null");

        var boringWordsSet = boringWords.ToHashSet();

        foreach (var boringWord in boringWordsSet)
        {
            if (string.IsNullOrEmpty(boringWord))
                return Result<None>.Fail("Каждое скучное слово должно быть непустой строкой и " +
                                         "не содержать спецсимволов и цифр");

            foreach (var c in boringWord)
                if (!char.IsLetter(c))
                    return Result<None>.Fail("Каждое скучное слово должно быть непустой строкой и " +
                                             "не содержать спецсимволов и цифр");
        }

        return Result<None>.Ok(null!);
    }
}