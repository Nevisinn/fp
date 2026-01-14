using TagsCloud.Infrastructure.Models;
using TagsCloud.Infrastructure.Selectors;

namespace TagsCloud.Infrastructure.Services.WordsProcessing.WordsHandlers;

public class BoringWordsHandler : IWordsHandler
{
    private readonly IWordsProviderSelector selector;

    public BoringWordsHandler(IWordsProviderSelector selector)
    {
        this.selector = selector;
    }

    public IWordsHandler? NextHandler { get; set; }
    public ProgramOptions Options { get; set; } = null!;

    public List<string> Handle(List<string> words)
    {
        var boringWordsFileExtension = Path.GetExtension(Options.InputBoringWordsFilePath).TrimStart('.').ToLower();
        var provider = selector.Select(boringWordsFileExtension);
        var boringWords = provider.ReadFile(Options.InputBoringWordsFilePath);

        ValidateBoringWords(boringWords);

        var handledWords = new List<string>();

        foreach (var word in words)
            if (!boringWords.Contains(word))
                handledWords.Add(word);

        return NextHandler != null ? NextHandler.Handle(handledWords) : handledWords;
    }

    private void ValidateBoringWords(List<string> boringWords)
    {
        if (boringWords == null) throw new ArgumentException("Список скучных слов не может быть null");

        var boringWordsSet = boringWords.ToHashSet();

        foreach (var boringWord in boringWordsSet)
        {
            if (string.IsNullOrEmpty(boringWord))
                throw new ArgumentException("Каждое скучное слово должно быть непустой строкой и " +
                                            "не содержать спецсимволов и цифр");

            foreach (var c in boringWord)
                if (!char.IsLetter(c))
                    throw new ArgumentException("Каждое скучное слово должно быть непустой строкой и " +
                                                "не содержать спецсимволов и цифр");
        }
    }
}