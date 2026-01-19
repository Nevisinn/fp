using FakeItEasy;
using FluentAssertions;
using TagsCloud.Core.Models;
using TagsCloud.Core.Selectors;
using TagsCloud.Core.Services.WordsProcessing.WordsHandlers;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class BoringWordsHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        A.CallTo(() => selector.Select(A<string>._, A<string>._))
            .Returns(Result<IWordsProvider>.Ok(provider));
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
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);
        var handledText = handleText.Value;

        handleText.IsSuccess.Should().BeTrue();
        handledText.Should().Equal("Hello", "KONTUR");
    }

    [Test]
    public void Handle_ShouldNotExcludeWithEmptyBoringWords()
    {
        boringWords = [];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);
        var handledText = handleText.Value;

        handleText.IsSuccess.Should().BeTrue();
        handledText.Should().Equal("Hello", "KONTUR", "test");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordIsNull()
    {
        boringWords = [null];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);

        handleText.IsSuccess.Should().BeFalse();
        handleText.Error.Should().Be("Каждое скучное слово должно быть непустой строкой и " +
                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordIsEmpty()
    {
        boringWords = [""];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);

        handleText.IsSuccess.Should().BeFalse();
        handleText.Error.Should().Be("Каждое скучное слово должно быть непустой строкой и " +
                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordHaveSpecialSymbols()
    {
        boringWords = ["t#e$x@t"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);

        handleText.IsSuccess.Should().BeFalse();
        handleText.Error.Should().Be("Каждое скучное слово должно быть непустой строкой и " +
                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordIsDigit()
    {
        boringWords = ["123"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);

        handleText.IsSuccess.Should().BeFalse();
        handleText.Error.Should().Be("Каждое скучное слово должно быть непустой строкой и " +
                                     "не содержать спецсимволов и цифр");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordsHaveDuplicate()
    {
        boringWords = ["test", "test"];
        A.CallTo(() => provider.ReadFile(A<string>._)).Returns(Result<List<string>>.Ok(boringWords));

        var handleText = boringWordsHandler.Handle(text);
        var handledText = handleText.Value;

        handleText.IsSuccess.Should().BeTrue();
        handledText.Should().Equal("Hello", "KONTUR");
    }

    [Test]
    public void Handle_ShouldReturnFailure_WhenBoringWordsIsNull()
    {
        boringWords = null;
        A.CallTo(() => provider.ReadFile(A<string>._))!.Returns(Result<List<string>>.Ok(boringWords));

        var handledText = boringWordsHandler.Handle(text);

        handledText.IsSuccess.Should().BeFalse();
        handledText.Error.Should().Be("Список скучных слов не может быть null");
    }
}