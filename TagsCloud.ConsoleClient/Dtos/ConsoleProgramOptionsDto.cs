using CommandLine;

namespace TagsCloud.Dtos;

public class ConsoleProgramOptionsDto
{
    [Option('p', "path", Required = true, HelpText = "Путь до файла со словами (txt, doc, docx)")]
    public required string InputWordsFilePath { get; init; }

    [Option("boringWordsPath", Required = true, HelpText = "Путь до файла со скучными словами (txt, doc, docx)")]
    public required string InputBoringWordsFilePath { get; init; }

    [Option('a', "algorithm", HelpText = "Алгоритм формирования облака (Circular)")]
    public string AlgorithmName { get; init; } = "Circular";

    [Option('f', "format", HelpText = "Формат изображения (Png, Jpg, Bmp)")]
    public string ImageFormat { get; init; } = "png";

    [Option("bg", HelpText = "Цвет заднего фона")]
    public string BackgroundColor { get; init; } = "black";

    [Option("cs", HelpText = "Стиль цвета текста (Solid, LinearGradient)")]
    public string ColorScheme { get; init; } = "Solid";

    [Option("width", HelpText = "Ширина изображения")]
    public int ImageWidth { get; init; } = 1000;

    [Option("height", HelpText = "Высота изображения")]
    public int ImageHeight { get; init; } = 1000;

    [Option("text-color", HelpText = "Цвет текста")]
    public string TextColor { get; init; } = "MidnightBlue";

    [Option("font-name", HelpText = "Название шрифта")]
    public string FontName { get; init; } = "Arial";

    [Option("font-size", HelpText = "Размер шрифта")]
    public int FontSize { get; init; } = 14;
}