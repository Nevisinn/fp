using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.ColorProvider;

public class ColorProvider : IColorProvider
{
    public Result<Color[]> GetColors(string[] names)
    {
        var colors = new List<Color>();
        foreach (var name in names)
        {   
            var getColor = GetColor(name);

            if (!getColor.IsSuccess)
                return Result<Color[]>.Fail(getColor.Error!);
            
            colors.Add(getColor.Value);
        }
        
        return Result<Color[]>.Ok(colors.ToArray());
    }

    public Result<Color> GetColor(string name)
    {
        var color = Color.FromName(name);

        if (!color.IsKnownColor) return Result<Color>.Fail($"Цвет {name} не найден");

        return Result<Color>.Ok(color);
    }
}