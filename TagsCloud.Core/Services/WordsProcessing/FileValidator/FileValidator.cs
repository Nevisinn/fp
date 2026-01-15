using TagsCloud.Core.Models;

namespace TagsCloud.Core.Services.WordsProcessing.FileValidator;

public class FileValidator : IFileValidator
{
    public Result<None> Validate(string path, string expectedExtension)
    {
        if (string.IsNullOrEmpty(path))
            return Result<None>.Fail("Путь до файла не валиден");

        if (!File.Exists(path)) return Result<None>.Fail("Файл не найден");

        var ext = Path.GetExtension(path).TrimStart('.').ToLower();
        if (expectedExtension != ext)
            return Result<None>.Fail($"Формат файла {ext} не поддерживается");

        return Result<None>.Ok(null!);
    }
}