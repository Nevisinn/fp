using System.Drawing;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.ColorProvider;

public class ColorProvider : IColorProvider
{
    public Color[] GetColors(string[] names)
    {
        var colors = new List<Color>();
        foreach (var name in names) colors.Add(GetColor(name));

        return colors.ToArray();
    }

    public Color GetColor(string name)
    {
        var color = Color.FromName(name);

        if (!color.IsKnownColor) throw new ArgumentException($"Цвет {name} не найден");

        return color;
    }
}