using FakeItEasy;
using FluentAssertions;
using TagsCloud.Infrastructure.Models;
using TagsCloud.Infrastructure.Selectors;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsHandlers;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class BoringWordsHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        A.CallTo(() => selector.Select(A<string>._)).Returns(provider);
    }

    private readonly List<string> text = ["Hello", "KONTUR", "test"];
    private readonly IWordsProviderSelector selector = A.Fake<IWordsProviderSelector>();
    private readonly IWordsProvider provider = A.Fake<IWordsProvider>();
    private readonly BoringWordsHandler boringWordsHandler;

    private readonly ProgramOptions options = new()
    {
        InputWordsFilePath = "",
        InputBoringWordsFilePath = "t.txt",
        ImageOptions = null
    };

    private List<string> boringWords;

    public BoringWordsHandlerTests()
    {
        boringWordsHandler = new BoringWordsHandler(selector)
        {
            Options = options
        };
    }

    [Test]
    public void Handle_ShouldExcludeBoringWords()
    {
        boringWords = ["test"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handledText = boringWordsHandler.Handle(text);

        handledText.Should().Equal("Hello", "KONTUR");
    }

    [Test]
    public void Handle_ShouldNotExcludeWithEmptyBoringWords()
    {
        boringWords = [];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handledText = boringWordsHandler.Handle(text);

        handledText.Should().Equal("Hello", "KONTUR", "test");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordIsNull()
    {
        boringWords = [null];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handleText = () => boringWordsHandler.Handle(text);

        handleText.Should().Throw<ArgumentException>("Каждое скучное слово должно быть непустой строкой и " +
                                                     "не содержать спецсимволов");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordIsEmpty()
    {
        boringWords = [""];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handleText = () => boringWordsHandler.Handle(text);

        handleText.Should().Throw<ArgumentException>("Каждое скучное слово должно быть непустой строкой и " +
                                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordHaveSpecialSymbols()
    {
        boringWords = ["t#e$x@t"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handleText = () => boringWordsHandler.Handle(text);

        handleText.Should().Throw<ArgumentException>("Каждое скучное слово должно быть непустой строкой и " +
                                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordIsDigit()
    {
        boringWords = ["123"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handleText = () => boringWordsHandler.Handle(text);

        handleText.Should().Throw<ArgumentException>("Каждое скучное слово должно быть непустой строкой и " +
                                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordsHaveDuplicate()
    {
        boringWords = ["test", "test"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(boringWords);

        var handledText = boringWordsHandler.Handle(text);

        handledText.Should().Equal("Hello", "KONTUR");
    }

    [Test]
    public void Handle_ShouldThrow_WhenBoringWordsIsNull()
    {
        boringWords = null;
        A.CallTo(() => provider.ReadFile(A<string>._))!.Returns(boringWords);

        var handledText = () => boringWordsHandler.Handle(text);

        handledText.Should().Throw<ArgumentException>("Список скучных слов не может быть null");
    }
}