using FluentAssertions;
using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

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
        var words = provider.ReadFile(filePath);

        words.Should().BeEquivalentTo(expectedWords);
    }

    [Test]
    public void ReadFile_ShouldThrow_WhenFileNotFound()
    {
        var missingPath = filePath + "missing";

        var readFile = () => provider.ReadFile(missingPath);

        readFile.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ReadFile_ShouldThrow_WhenFileIsEmpty()
    {
        writer.WriteText(filePath, string.Empty);

        var readFile = () => provider.ReadFile(filePath);

        readFile.Should().Throw<InvalidDataException>("Файл пуст");
    }

    [Test]
    public void ReadFile_ShouldThrow_WhenFilePathIsNull()
    {
        filePath = null;

        var readFile = () => provider.ReadFile(filePath);

        readFile.Should().Throw<ArgumentException>("Путь до файла не валиден");
    }

    [Test]
    public void ReadFile_ShouldThrow_WhenFilePathIsEmpty()
    {
        filePath = "";

        var readFile = () => provider.ReadFile(filePath);

        readFile.Should().Throw<ArgumentException>("Путь до файла не валиден");
    }
}