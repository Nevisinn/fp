using System.Drawing;
using TagsCloud.Dtos;
using TagsCloud.Infrastructure.Models;
using TagsCloud.Infrastructure.Selectors;
using TagsCloud.Infrastructure.Services.ImageGeneration;
using TagsCloud.Infrastructure.Services.ImageGeneration.ColorProvider;
using TagsCloud.Infrastructure.Services.ImageGeneration.FontProviders;

namespace TagsCloud;

public class ProgramOptionsMapper
{
    private readonly IAlgorithmSelector algorithmSelector;
    private readonly IColorProvider colorProvider;
    private readonly IColorSchemeSelector colorSchemeSelector;
    private readonly IFontProvider fontProvider;
    private readonly IWordsProviderSelector wordsProviderSelector;

    public ProgramOptionsMapper
    (
        IColorProvider colorProvider,
        IFontProvider fontProvider,
        IColorSchemeSelector colorSchemeSelector,
        IWordsProviderSelector wordsProviderSelector,
        IAlgorithmSelector algorithmSelector)
    {
        this.colorProvider = colorProvider;
        this.fontProvider = fontProvider;
        this.colorSchemeSelector = colorSchemeSelector;
        this.wordsProviderSelector = wordsProviderSelector;
        this.algorithmSelector = algorithmSelector;
    }

    public ProgramOptions Map(ConsoleProgramOptionsDto dto)
    {
        return new ProgramOptions
        {
            InputWordsFilePath = dto.InputWordsFilePath,
            InputBoringWordsFilePath = dto.InputBoringWordsFilePath,
            ImageOptions = new ImageOptions
            {
                BackgroundColor = colorProvider.GetColor(dto.BackgroundColor),
                ColorScheme = colorSchemeSelector.Select(dto.ColorScheme),
                Font = fontProvider.GetFont(dto.FontName, dto.FontSize),
                ImageFormat = ImageFormatParser.Parse(dto.ImageFormat),
                ImageSize = new Size(dto.ImageWidth, dto.ImageHeight),
                TextColors = colorProvider.GetColors(dto.TextColor.Split(',').ToArray())
            },
            Algorithm = algorithmSelector.Select(dto.AlgorithmName),
            WordsProvider = wordsProviderSelector.Select(Path.GetExtension(dto.InputWordsFilePath).Trim('.'))
        };
    }
}