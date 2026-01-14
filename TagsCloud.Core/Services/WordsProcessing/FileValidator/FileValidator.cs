namespace TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;

public class FileValidator : IFileValidator
{
    public void Validate(string path, string expectedExtension)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("Путь до файла не валиден");

        if (!File.Exists(path)) throw new FileNotFoundException("Файл не найден");

        var ext = Path.GetExtension(path).TrimStart('.').ToLower();
        if (expectedExtension != ext)
            throw new NotSupportedException($"Формат файла {ext} не поддерживается");
    }
}