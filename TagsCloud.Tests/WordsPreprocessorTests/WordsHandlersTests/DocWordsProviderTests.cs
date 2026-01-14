using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class DocWordsProviderTests : WordsProviderTests
{
    public DocWordsProviderTests() : base(new DocWordsProvider(new FileValidator()), new DocWriter())
    {
    }
}