using FluentAssertions;
using TechTolk.Compiling.Compiler;

namespace TechTolk.IntegrationTests;

public class TolkCompilerTests
{
    [Fact]
    public void Can_compile_string_translation_sets()
    {
        var tolk = TolkCompilation.ForType<string>()
            .WithDivider(null)
            .WithMerger(null)
            .WithTranslationSetConverter(null)
            .AddTranslationSet(null)
            .AddTranslationSet(null).DiscardDuplicatesOnMerge()
            .Compile();

        var result = tolk.Translate("TestTranslationKey");

        result.Should().Be("TranslationResult");
    }
}