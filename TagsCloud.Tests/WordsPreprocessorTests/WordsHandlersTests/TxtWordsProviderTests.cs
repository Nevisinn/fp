using TagsCloud.Infrastructure.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Infrastructure.Services.WordsProcessing.FileValidator;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class TxtWordsProviderTests : WordsProviderTests
{
    public TxtWordsProviderTests() : base(new TxtWordsProvider(new FileValidator()), new TxtWriter())
    {
    }
}