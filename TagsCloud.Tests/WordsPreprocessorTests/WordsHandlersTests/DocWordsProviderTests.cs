using TagsCloud.Core.Services.WordsProcessing.DocumentWriters;
using TagsCloud.Core.Services.WordsProcessing.FileValidator;
using TagsCloud.Core.Services.WordsProcessing.WordsProviders;

namespace TagsCloud.Test.WordsPreprocessorTests.WordsHandlersTests;

[TestFixture]
public class DocWordsProviderTests : WordsProviderTests
{
    public DocWordsProviderTests() : base(new DocWordsProvider(new FileValidator()), new DocWriter())
    {
    }
}