using System.Drawing;

namespace TagsCloud.Infrastructure.Services.ImageGeneration.ColorProvider;

public interface IColorProvider
{
    public Color[] GetColors(string[] names);
    public Color GetColor(string name);
}