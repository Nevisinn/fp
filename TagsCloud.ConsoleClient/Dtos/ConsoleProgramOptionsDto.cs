using CommandLine;

namespace TagsCloud.Dtos;

public class ConsoleProgramOptionsDto
{
    [Option($"{nameof(InputWordsFilePath)}", Required = true, HelpText = "Путь до файла со словами (txt, doc, docx)")]
    public required string InputWordsFilePath { get; init; }

    [Option($"{nameof(BoringWordsFilePath)}", Required = true,
        HelpText = "Путь до файла со скучными словами (txt, doc, docx)")]
    public required string BoringWordsFilePath { get; init; }

    [Option($"{nameof(AlgorithmName)}", HelpText = "Алгоритм формирования облака (Circular)")]
    public string AlgorithmName { get; init; } = "Circular";

    [Option($"{nameof(ImageFormat)}", HelpText = "Формат изображения (Png, Jpg, Bmp)")]
    public string ImageFormat { get; init; } = "png";

    [Option($"{nameof(BackgroundColor)}", HelpText = "Цвет заднего фона")]
    public string BackgroundColor { get; init; } = "black";

    [Option($"{nameof(ColorScheme)}", HelpText = "Стиль цвета текста (Solid, LinearGradient)")]
    public string ColorScheme { get; init; } = "Solid";

    [Option($"{nameof(ImageWidth)}", HelpText = "Ширина изображения")]
    public int ImageWidth { get; init; } = 1000;

    [Option($"{nameof(ImageHeight)}", HelpText = "Высота изображения")]
    public int ImageHeight { get; init; } = 1000;

    [Option($"{nameof(TextColor)}", HelpText = "Цвет текста")]
    public string TextColor { get; init; } = "MidnightBlue";

    [Option($"{nameof(FontName)}", HelpText = "Название шрифта")]
    public string FontName { get; init; } = "Arial";

    [Option($"{nameof(FontSize)}", HelpText = "Размер шрифта")]
    public int FontSize { get; init; } = 14;
}