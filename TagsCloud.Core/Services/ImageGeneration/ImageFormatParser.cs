using System.Drawing.Imaging;

namespace TagsCloud.Infrastructure.Services.ImageGeneration;

public class ImageFormatParser
{
    public static ImageFormat Parse(string format)
    {
        if (string.IsNullOrEmpty(format))
            throw new ArgumentException("Формат не может быть пустым");

        return format.ToLowerInvariant() switch
        {
            "png" => ImageFormat.Png,
            "jpg" => ImageFormat.Jpeg,
            "jpeg" => ImageFormat.Jpeg,
            "bmp" => ImageFormat.Bmp,
            _ => throw new ArgumentException($"Неподдерживаемый формат: {format}")
        };
    }
}