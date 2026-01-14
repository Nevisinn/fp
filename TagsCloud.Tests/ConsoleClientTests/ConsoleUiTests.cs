using System.Reflection;
using Autofac;
using FluentAssertions;
using TagsCloud.Modules;

namespace TagsCloud.Test.ConsoleClientTests;

[TestFixture]
public class ConsoleUiTests
{
    [SetUp]
    public void SetUp()
    {
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(AppModule))!);
        container = builder.Build();
    }

    [TearDown]
    public void TearDown()
    {
        container.Dispose();
        File.Delete(outputImagePath);
    }

    private readonly string inputFilePath;
    private readonly string boringWordsFilePath;
    private readonly string imagesDir;
    private string outputImagePath;
    private IContainer container;

    public ConsoleUiTests()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        inputFilePath = Path.Combine($"{projectDirectory}", "ConsoleClientTests", "input.txt");
        boringWordsFilePath = Path.Combine($"{projectDirectory}", "ConsoleClientTests", "boringWords.txt");
        imagesDir = Path.Combine($"{projectDirectory}", "Images");
        outputImagePath = "";
    }

    [Test]
    public void Run_ShouldCreateTagsCloudImage_WithDefaultParameters()
    {
        var args = new[]
        {
            "--p", $"{inputFilePath}",
            "--boringWordsPath", $"{boringWordsFilePath}"
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
        var fileInfo = new FileInfo(outputImagePath);
        fileInfo.Length.Should().BeGreaterThan(0);
    }

    [TestCase("Png")]
    [TestCase("Jpeg")]
    [TestCase("Bmp")]
    public void Run_ShouldCreateTagsCloudImage_WithDifferentImageFormats(string format)
    {
        var args = new[]
        {
            "--p", inputFilePath,
            "--boringWordsPath", $"{boringWordsFilePath}",
            "--f", format
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
        var extension = Path.GetExtension(outputImagePath).TrimStart('.');
        extension.Should().Be(format);
    }

    [TestCase("Circular")]
    public void Run_ShouldCreateImage_WithCircularAlgorithm(string algorithm)
    {
        var args = new[]
        {
            "--p", inputFilePath,
            "--boringWordsPath", $"{boringWordsFilePath}",
            "--a", algorithm
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
    }

    [TestCase("Black")]
    [TestCase("White")]
    [TestCase("Gray")]
    public void Run_ShouldCreateImage_WithBackgroundColor(string bgColor)
    {
        var args = new[]
        {
            "--path", inputFilePath,
            "--boringWordsPath", $"{boringWordsFilePath}",
            "--bg", bgColor
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
    }

    [TestCase("Red")]
    [TestCase("Blue")]
    [TestCase("Green")]
    public void Run_ShouldCreateImage_WithTextColor(string textColor)
    {
        var args = new[]
        {
            "--path", inputFilePath,
            "--boringWordsPath", $"{boringWordsFilePath}",
            "--text-color", textColor
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
    }

    [TestCase("Arial")]
    [TestCase("Times New Roman")]
    [TestCase("Courier New")]
    public void Run_ShouldCreateImage_WithFontName(string fontName)
    {
        var args = new[]
        {
            "--path", inputFilePath,
            "--boringWordsPath", $"{boringWordsFilePath}",
            "--font-name", fontName
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
    }
}