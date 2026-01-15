using System.Drawing.Imaging;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration;

public class ImageFormatParser
{
    public static Result<ImageFormat> Parse(string format)
    {
        if (string.IsNullOrEmpty(format))
            return Result<ImageFormat>.Fail("Формат не может быть пустым");

        return format.ToLowerInvariant() switch
        {
            "png" => Result<ImageFormat>.Ok(ImageFormat.Png),
            "jpeg" => Result<ImageFormat>.Ok(ImageFormat.Jpeg),
            "bmp" => Result<ImageFormat>.Ok(ImageFormat.Bmp),
            _ => throw new ArgumentException($"Неподдерживаемый формат: {format}")
        };
    }
}