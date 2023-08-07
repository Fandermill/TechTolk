using TechTolk;
using TechTolk.Division.Internals;
using TechTolkTests.Helpers;

namespace TechTolkTests;

public class MergingTests
{
    /*private readonly StringDivider _divider;
    private readonly ITranslationSetMerger _merger;

    private readonly ITranslationSet _set1;
    private readonly ITranslationSet _set2;


    public MergingTests()
    {
        _divider = new StringDivider("nl");
        var supportedDividers = new SupportedDividersProvider();
        supportedDividers.AddSupportedDivider(_divider);

        _merger = new TranslationSetMerger(
            new TranslationSetFactory(),
            supportedDividers);

        _set1 = new TranslationSetBuilder()
            .ForDivider(_divider)
            .Add("duplicate.key", "1st-value")
            .Build();

        _set2 = new TranslationSetBuilder()
            .ForDivider(_divider)
            .Add("duplicate.key", "2nd-value")
            .Build();
    }

    [Fact]
    public void Merge_translation_sets_with_replace_duplicate_behavior()
    {
        var options = new TranslationSetMergerOptions { MergeBehavior = DuplicateBehavior.Replace };

        var result = _merger.Merge(options, _set1, _set2);

        var value = result.GetTranslation(_divider.Key, "duplicate.key");
        value.Should().Be("2nd-value");
    }

    [Fact]
    public void Merge_translation_sets_with_discard_duplicate_behavior()
    {
        var options = new TranslationSetMergerOptions { MergeBehavior = DuplicateBehavior.Discard };

        var result = _merger.Merge(options, _set1, _set2);

        var value = result.GetTranslation(_divider.Key, "duplicate.key");
        value.Should().Be("1st-value");
    }

    [Fact]
    public void Merge_translation_sets_with_throw_duplicate_behavior()
    {
        var options = new TranslationSetMergerOptions { MergeBehavior = DuplicateBehavior.Throw };

        var act = () => _merger.Merge(options, _set1, _set2);
        act.Should().Throw<ArgumentException>(); // todo - make custom exception
    }*/
}
