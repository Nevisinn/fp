using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class DocxWordsProviderTests : WordsProviderTests
{
    public DocxWordsProviderTests() : base(new DocxWordsProvider(new FileValidator()), new DocxWriter())
    {
    }
}