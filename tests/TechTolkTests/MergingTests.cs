using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolk.Exceptions;
using TechTolk.Registration.Builders;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class MergingTests : AbstractTechTolkTests
{
    private const string MergedSetKey = "MergedSet";

    [Fact]
    public void Duplicate_key_in_subsequent_translation_set_will_replace_the_previous_value_as_configured()
    {
        SetupMergedTranslationSetWithOptions(options => options.OnDuplicateKey().Replace());
        var tolk = GetTolkForTranslationSet(MergedSetKey);

        var result = tolk.Translate(DividerConstants.NL, "MyKey");

        result.Should().Be("NL-MyValue-FromSet2");
    }

    [Fact]
    public void Duplicate_key_in_subsequent_translation_set_will_be_discarded_as_configured()
    {
        SetupMergedTranslationSetWithOptions(options => options.OnDuplicateKey().Discard());
        var tolk = GetTolkForTranslationSet(MergedSetKey);

        var result = tolk.Translate(DividerConstants.NL, "MyKey");

        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Duplicate_key_in_subsequent_translation_set_will_throw_exception_as_configured()
    {
        SetupMergedTranslationSetWithOptions(options => options.OnDuplicateKey().ThrowException());
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(MergedSetKey);

        act.Should().Throw<DuplicateTranslationKeyException>();
    }

    [Fact]
    public void New_key_in_subsequent_translation_set_will_be_added_to_merged_set()
    {
        SetupMergedTranslationSetWithOptions(null);
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();
        var tolk = tolkFactory.Create(MergedSetKey);

        var result = tolk.Translate(DividerConstants.NL, "AdditionalKey");

        result.Should().Be("NL-AdditionalValue");
    }

    private void SetupMergedTranslationSetWithOptions(Action<IMergedTranslationSetOptionsBuilder>? options)
    {
        _services.AddTechTolk()
            .ConfigureDefaultDividers()
            .AddMergedTranslationSet("MergedSet", builder =>
            {
                builder.FromSource(Set1.Key, new Set1());
                builder.FromSource(Set2.Key, new Set2());
                if (options is not null)
                    builder.WithOptions(options);
            });
    }
}
