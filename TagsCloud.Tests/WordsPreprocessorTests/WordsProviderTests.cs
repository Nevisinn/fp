using FluentAssertions;
using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests;

public abstract class WordsProviderTests
{
    private readonly IWordsProvider provider;
    private readonly IDocumentWriter writer;
    private string filePath;

    public WordsProviderTests(IWordsProvider provider, IDocumentWriter writer)
    {
        this.provider = provider;
        this.writer = writer;
        var currentContext = TestContext.CurrentContext;
        var workingDirectory = currentContext.WorkDirectory;
        var currentProject = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        filePath = Path.Combine(currentProject, $"test.{provider.FileFormat}");
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    [Test]
    public void ReadFile_ShouldReturnExpectedWords()
    {
        var inputText = "Hello\nKontur\ntest";
        var expectedWords = new List<string>
        {
            "Hello", "Kontur", "test"
        };

        writer.WriteText(filePath, inputText);

        var readFile = provider.ReadFile(filePath);
        var words = readFile.Value;

        readFile.IsSuccess.Should().BeTrue();
        words.Should().BeEquivalentTo(expectedWords);
    }

    [Test]
    public void ReadFile_ShouldReturnFailure_WhenFileNotFound()
    {
        var missingPath = filePath + "missing";

        var readFile = provider.ReadFile(missingPath);

        readFile.IsSuccess.Should().BeFalse();
        readFile.Error.Should().Be("Файл не найден");
    }

    [Test]
    public void ReadFile_ShouldReturnFailure_WhenFileIsEmpty()
    {
        writer.WriteText(filePath, string.Empty);

        var readFile = provider.ReadFile(filePath);

        readFile.IsSuccess.Should().BeFalse();
        readFile.Error.Should().Be("Файл пуст");
    }

    [Test]
    public void ReadFile_ShouldReturnFailure_WhenFilePathIsNull()
    {
        filePath = null;

        var readFile = provider.ReadFile(filePath);

        readFile.IsSuccess.Should().BeFalse();
        readFile.Error.Should().Be("Путь до файла не валиден");
    }

    [Test]
    public void ReadFile_ShouldReturnFailure_WhenFilePathIsEmpty()
    {
        filePath = "";

        var readFile = provider.ReadFile(filePath);

        readFile.IsSuccess.Should().BeFalse();
        readFile.Error.Should().Be("Путь до файла не валиден");
    }
}