using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class DocxWordsProviderTests : WordsProviderTests
{
    public DocxWordsProviderTests() : base(new DocxWordsProvider(new FileValidator()), new DocxWriter())
    {
    }
}