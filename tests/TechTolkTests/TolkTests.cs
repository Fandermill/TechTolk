using System.Globalization;
using TechTolk;
using TechTolk.Exceptions;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class TolkTests : AbstractTechTolkTests
{
    [Fact]
    public void Tolk_returns_correct_value_for_divider()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var nlResult = tolk.Translate(DividerConstants.NL, "KeyThatExistsInAllDividers");
        var enResult = tolk.Translate(DividerConstants.EN, "KeyThatExistsInAllDividers");

        nlResult.Should().Be("NL-ValueFromKeyThatExistsInAllDividers");
        enResult.Should().Be("EN-ValueFromKeyThatExistsInAllDividers");
    }

    [Fact]
    public void Tolk_returns_value_from_current_divider_when_no_divider_is_given()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");
        var result = tolk.Translate("KeyThatExistsInAllDividers");

        result.Should().Be("NL-ValueFromKeyThatExistsInAllDividers");
    }

    [Fact]
    public void Tolk_throws_exception_for_missing_translation_key_as_configured_globally()
    {
        _services.AddTechTolk(options => options.OnTranslationNotFound().ThrowException())
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var action = () => tolk.Translate(DividerConstants.NL, "MissingTranslationKey");

        action.Should().ThrowExactly<TranslationKeyNotFoundException>();
    }

    [Fact]
    public void Tolk_returns_translation_key_for_missing_translation_key_as_configured_globally()
    {
        _services.AddTechTolk(options => options.OnTranslationNotFound().ReturnTranslationKey())
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var result = tolk.Translate(DividerConstants.NL, "MissingTranslationKey");

        result.Should().Be("MissingTranslationKey");
    }

    [Fact]
    public void Tolk_returns_empty_string_for_missing_translation_key_as_configured_globally()
    {
        _services.AddTechTolk(options => options.OnTranslationNotFound().ReturnEmptyString())
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var result = tolk.Translate(DividerConstants.NL, "MissingTranslationKey");

        result.Should().Be(string.Empty);
    }

    [Fact]
    public void Tolk_returns_translation_key_for_missing_translation_key_as_configured_specifically_for_translation_set()
    {
        // TODO - Do we really need separate tests for Throw/EmptyString ?

        _services.AddTechTolk(options => options.OnTranslationNotFound().ReturnEmptyString())
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s =>
            {
                s.FromSource(new Set1());
                s.WithOptions(options => options.OnTranslationNotFound().ReturnTranslationKey());
            });

        var tolk = GetTolkForTranslationSet(Set1.Key);

        var result = tolk.Translate(DividerConstants.NL, "MissingTranslationKey");

        result.Should().Be("MissingTranslationKey");
    }
}
