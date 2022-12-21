using FluentAssertions;
using TechTolk.Compiling.Compiler;
using TechTolk.Compiling.Converting;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Parsing;
using TechTolk.Compiling.Sourcing;
using TechTolk.Tests.Shared;
using TechTolk.Translations;

namespace TechTolk.IntegrationTests;

public class TolkCompilerTests
{
    [Fact]
    public void Can_compile_string_translation_sets()
    {
        var fixedDivider = new FixedStringDivider("nl");
        var recordSet = new TranslationRecordSet<string>("TestingSet");
        recordSet.AddRecord(new TranslationRecord<string>(fixedDivider, "TestTranslationKey", "TranslationResult"));
        recordSet.AddRecord(new TranslationRecord<string>(fixedDivider, "TestTranslationKey", "DuplicateResult"));


        var tolk = TolkCompilation.ForType<string>()
            .WithCurrentDividerProvider(new FixedDividerProvider(fixedDivider))
            .WithMerger(new TranslationRecordSetMerger<string>())
            .WithTranslationSetConverter(new TranslationSetConverter<string>(
                new StringTranslationValueParser()))
            .AddTranslationSet(() => recordSet).DiscardDuplicatesOnMerge()
            .Compile();

        // Veel te veel, bah. Er moeten defaults komen. Desnoods in een
        // dictionary per generic T

        var result = tolk.Translate("TestTranslationKey");

        result.Should().Be("TranslationResult");
    }
}