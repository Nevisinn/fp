using System.Reflection;
using Autofac;
using FluentAssertions;
using TagsCloud.Dtos;
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
        if (File.Exists(outputImagePath))
            File.Delete(outputImagePath);
    }

    private readonly string imagesDir;
    private string outputImagePath;
    private IContainer container;
    private readonly ConsoleProgramOptionsDto dto;

    public ConsoleUiTests()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        imagesDir = Path.Combine($"{projectDirectory}", "Images");
        outputImagePath = "";
        dto = new ConsoleProgramOptionsDto
        {
            InputWordsFilePath = Path.Combine($"{projectDirectory}", "ConsoleClientTests", "input.txt"),
            BoringWordsFilePath = Path.Combine($"{projectDirectory}", "ConsoleClientTests", "boringWords.txt")
        };
    }

    [Test]
    public void Run_ShouldCreateTagsCloudImage_WithDefaultParameters()
    {
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath
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
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.ImageFormat)}", format
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
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.AlgorithmName)}", algorithm
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
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.BackgroundColor)}", bgColor
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
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.TextColor)}", textColor
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
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.FontName)}", fontName
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var files = Directory.GetFiles(imagesDir);
        files.Should().ContainSingle();
        outputImagePath = files[0];
    }

    [TestCase("qasdasd")]
    public void Run_ShouldPrintFail_WithNotExistFontName(string fontName)
    {
        var writer = new StringWriter();
        Console.SetOut(writer);
        var message = $"Шрифт {fontName} не найден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.FontName)}", fontName
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("adasd")]
    public void Run_ShouldPrintFail_WithNotExistTextColor(string textColor)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = $"Цвет {textColor} не найден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.TextColor)}", textColor
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("qwe")]
    public void Run_ShouldPrintFail_WithNotExistAlgorithm(string algorithm)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = $"Параметр \"{nameof(dto.AlgorithmName)}: {algorithm}\" не валиден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.AlgorithmName)}", algorithm
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("abc")]
    public void Run_ShouldPrintFail_WithNotExistImageFormats(string format)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = $"Неподдерживаемый формат: {format}";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.ImageFormat)}", format
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("qwe")]
    [TestCase("qwe.abc")]
    public void Run_ShouldPrintFail_WithInvalidPathExtension(string inputWordsFilePath)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var extension = Path.GetExtension(inputWordsFilePath).TrimStart('.');
        var message = $"Параметр \"{nameof(dto.InputWordsFilePath)}: {extension}\" не валиден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", inputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("abc.txt")]
    public void Run_ShouldPrintFail_WithNotExistPath(string inputWordsFilePath)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = "Файл не найден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", inputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase("abc.txt")]
    public void Run_ShouldPrintFail_WithNotExistBoringWordsPath(string boringWordsFilePath)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = "Файл не найден";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", boringWordsFilePath
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase(50, 50)]
    public void Run_ShouldPrintFail_WithSmallImageSize(int imageWidth, int imageHeight)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = "Облако тегов не влезло в изображение заданного размера";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.ImageWidth)}", imageWidth.ToString(),
            $"--{nameof(dto.ImageHeight)}", imageHeight.ToString()
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }

    [TestCase(-50)]
    public void Run_ShouldPrintFail_WithInvalidFontSize(int fontSize)
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        var message = "Размер шрифта должен быть больше нуля";
        var args = new[]
        {
            $"--{nameof(dto.InputWordsFilePath)}", dto.InputWordsFilePath,
            $"--{nameof(dto.BoringWordsFilePath)}", dto.BoringWordsFilePath,
            $"--{nameof(dto.FontSize)}", fontSize.ToString()
        };
        var consoleUi = container.Resolve<ConsoleUi>();

        consoleUi.Run(args);

        var output = writer.ToString().TrimEnd('\r', '\n');
        output.Should().Be(message);
    }
}