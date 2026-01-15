using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.WordsProcessing.FileValidator;

public interface IFileValidator
{
    public Result<None> Validate(string path, string expectedExtension);
}