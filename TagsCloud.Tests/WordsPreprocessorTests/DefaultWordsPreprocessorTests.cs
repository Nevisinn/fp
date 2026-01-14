using FluentAssertions;
using TagsCloud.Infrastructure.Services.WordsProcessing.WordsPreprocessors;

namespace TagsCloud.Test.WordsPreprocessorTests;

[TestFixture]
public abstract class DefaultWordsPreprocessorTests
{
    [Test]
    public void Handle_ShouldThrowArgumentNullException_WhenHandlerIsNull()
    {
        var createPreprocessor = () => new DefaultWordsPreprocessor(null);

        createPreprocessor.Should().Throw<ArgumentNullException>();
    }
}