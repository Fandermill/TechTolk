using TechTolk.Compiling;

namespace TechTolk.UnitTests.Compiling;

public class TolkBuilderTests
{
    [Fact]
    public void Can_add_single_translation_set()
    {
        // TODO - Write compilation tests... and not in this class... =)


        TolkCompiler.ForType<string>()
            .WithDivider(new FixedDividerProvider(new FixedStringDivider("nl")))
            .WithMerger(null) // todo
            .AddTranslationSet(() => new TranslationSet<string>("tst1")).DiscardDuplicates()
            .AddTranslationSet(() => new TranslationSet<string>("test 2"))
            .Compile();

        throw new NotImplementedException();

    }
}
