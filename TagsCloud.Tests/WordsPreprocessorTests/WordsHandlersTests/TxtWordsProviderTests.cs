using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class TxtWordsProviderTests : WordsProviderTests
{
    public TxtWordsProviderTests() : base(new TxtWordsProvider(new FileValidator()), new TxtWriter())
    {
    }
}