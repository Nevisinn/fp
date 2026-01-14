namespace TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;

public interface IFileValidator
{
    public void Validate(string path, string expectedExtension);
}