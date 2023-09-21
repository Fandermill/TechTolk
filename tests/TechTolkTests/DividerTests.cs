using TechTolk;
using TechTolk.Exceptions;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class DividerTests : AbstractTechTolkTests
{
    [Fact]
    public void Not_registering_supported_dividers_will_throw_an_exception()
    {
        _services.AddTechTolk().AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var act = () => GetTolkForTranslationSet(Set1.Key);

        act.Should().Throw<RegistrationException>();
    }

    [Fact]
    public void Registering_a_supported_divider_twice_will_throw_an_exception()
    {
        var act = () => _services.AddTechTolk()
            .ConfigureDividers(config =>
            {
                config.AddSupportedDivider(DividerConstants.NL);
                config.AddSupportedDivider(DividerConstants.NL);
            });

        act.Should().Throw<RegistrationException>();
    }

    [Fact]
    public void Requesting_a_translation_with_an_unknown_divider_will_throw_an_exception()
    {
        _services.AddTechTolk()
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var act = () => tolk.Translate(new StringDivider("ZZ"), "MyKey");

        act.Should().Throw<DividerNotFoundException>();
    }

    [Fact]
    public void Registering_translations_with_an_unsupported_divider_will_throw_an_exception()
    {
        _services.AddTechTolk()
            .ConfigureDividers(config => config.AddSupportedDivider(DividerConstants.NL))
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var act = () => GetTolkForTranslationSet(Set1.Key);

        act.Should().Throw<UnsupportedDividerException>();
    }

    [Fact]
    public void Tolk_uses_custom_current_divider_provider_if_configured_so()
    {
        _services.AddTechTolk().ConfigureDividers(config =>
        {
            config.AddSupportedDivider(DividerConstants.FixedDivider);
            config.SetCurrentDividerProvider<FixedDividerProvider>(p => new FixedDividerProvider(DividerConstants.FixedDivider.Key));
        }).AddTranslationSet(SetForFixedDivider.Key, s => s.FromSource(new SetForFixedDivider()));
        var tolk = GetTolkForTranslationSet(SetForFixedDivider.Key);

        var result = tolk.Translate("MyFixedDividerKey");

        result.Should().Be("MyFixedDividerValue");
    }
}
