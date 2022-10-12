using TechTolk.Compiling.Compiler;
using TechTolk.Compiling.Converting;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Sourcing;

namespace TechTolk.UnitTests.Compiling;

public class TolkBuilderTests
{
    [Fact]
    public void Can_add_single_translation_set()
    {
        // TODO - Write compilation tests... and not in this class... =)

        var recordSet = new TranslationRecordSet<string>("test-set-1");
        recordSet.AddRecord(new TranslationRecord<string>(
            new FixedStringDivider("nl"), 
            "RECORD_ONE", 
            "Translation one")
            .SetSource(recordSet));

        TolkCompilation.ForType<string>()
            .WithDivider(new FixedDividerProvider(new FixedStringDivider("nl")))
            .WithMerger(new TranslationRecordSetMerger<string>()) // todo
            .WithTranslationSetConverter(new TranslationSetConverter<string>()) // todo
            .AddTranslationSet(() => recordSet)
            .AddTranslationSet(() =>
            {
                var recordSet2 = new TranslationRecordSet<string>("test-set-2");
                recordSet2.AddRecord(
                    new TranslationRecord<string>(
                        new FixedStringDivider("nl"), 
                        "RECORD_TWO", 
                        "Translation two")
                    .SetSource(recordSet2));
                return recordSet2;
            }).DiscardDuplicatesOnMerge()
            .Compile();

        throw new NotImplementedException();

    }
}
