using System.Drawing;
using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.ImageGeneration.ColorProvider;

public interface IColorProvider
{
    public Result<Color[]> GetColors(string[] names);
    public Result<Color> GetColor(string name);
}