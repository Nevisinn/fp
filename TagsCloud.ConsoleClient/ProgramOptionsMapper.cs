using System.Drawing;
using TagsCloud.Core.Models;
using TagsCloud.Core.Selectors;
using TagsCloud.Core.Services.ImageGeneration;
using TagsCloud.Core.Services.ImageGeneration.ColorProvider;
using TagsCloud.Core.Services.ImageGeneration.FontProviders;
using TagsCloud.Dtos;

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

    public Result<ProgramOptions> Map(ConsoleProgramOptionsDto dto)
    {
        var provideBackgroundColor = colorProvider.GetColor(dto.BackgroundColor);
        if (!provideBackgroundColor.IsSuccess)
            return Result<ProgramOptions>.Fail(provideBackgroundColor.Error!);

        var colorSchemeSelect = colorSchemeSelector
            .Select(dto.ColorScheme, nameof(dto.ColorScheme));

        if (!colorSchemeSelect.IsSuccess)
            return Result<ProgramOptions>.Fail(colorSchemeSelect.Error!);

        var provideTextColor = colorProvider.GetColors(dto.TextColor.Split(','));
        if (!provideTextColor.IsSuccess)
            return Result<ProgramOptions>.Fail(provideTextColor.Error!);

        var selectAlgorithm = algorithmSelector.Select(dto.AlgorithmName, nameof(dto.AlgorithmName));
        if (!selectAlgorithm.IsSuccess)
            return Result<ProgramOptions>.Fail(selectAlgorithm.Error!);

        var provideWords = wordsProviderSelector
            .Select(Path.GetExtension(dto.InputWordsFilePath).Trim('.'), nameof(dto.InputWordsFilePath));

        if (!provideWords.IsSuccess)
            return Result<ProgramOptions>.Fail(provideWords.Error!);

        var parseImageFormat = ImageFormatParser.Parse(dto.ImageFormat);
        if (!parseImageFormat.IsSuccess)
            return Result<ProgramOptions>.Fail(parseImageFormat.Error!);

        var getFont = fontProvider.GetFont(dto.FontName, dto.FontSize);
        if (!getFont.IsSuccess)
            return Result<ProgramOptions>.Fail(getFont.Error!);

        if (dto.ImageWidth <= 0 || dto.ImageHeight <= 0)
            return Result<ProgramOptions>.Fail("Ширина и высота изображения должна быть больше нуля");

        var options = new ProgramOptions
        {
            InputWordsFilePath = dto.InputWordsFilePath,
            InputBoringWordsFilePath = dto.BoringWordsFilePath,
            ImageOptions = new ImageOptions
            {
                BackgroundColor = provideBackgroundColor.Value,
                ColorScheme = colorSchemeSelect.Value!,
                Font = getFont.Value!,
                ImageFormat = parseImageFormat.Value!,
                ImageSize = new Size(dto.ImageWidth, dto.ImageHeight),
                TextColors = provideTextColor.Value!
            },
            Algorithm = selectAlgorithm.Value!,
            WordsProvider = provideWords.Value!
        };

        return Result<ProgramOptions>.Ok(options);
    }
}