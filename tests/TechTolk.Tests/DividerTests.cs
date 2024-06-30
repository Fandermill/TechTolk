using TechTolk.Exceptions;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests;

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
                config.AddSupportedDivider(Constants.CultureInfoDividers.nl_NL);
                config.AddSupportedDivider(Constants.CultureInfoDividers.nl_NL);
            });

        act.Should().Throw<RegistrationException>();
    }

    [Fact]
    public void Requesting_a_translation_with_an_unknown_divider_will_throw_an_exception()
    {
        _services.AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var act = () => tolk.Translate(new StringDivider("ZZ"), "MyKey");

        act.Should().Throw<DividerNotFoundException>();
    }

    [Fact]
    public void Registering_translations_with_an_unsupported_divider_will_throw_an_exception()
    {
        _services.AddTechTolk()
            .ConfigureDividers(config => config.AddSupportedDivider(Constants.CultureInfoDividers.nl_NL))
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var act = () => GetTolkForTranslationSet(Set1.Key);

        act.Should().Throw<UnsupportedDividerException>();
    }

    [Fact]
    public void Tolk_uses_custom_current_divider_provider_if_configured_so()
    {
        _services.AddTechTolk().ConfigureDividers(config =>
        {
            config.AddSupportedDivider(Constants.StringDividers.FixedDivider);
            config.SetCurrentDividerProvider<FixedDividerProvider>(p => new FixedDividerProvider(Constants.StringDividers.FixedDivider.Key));
        }).AddTranslationSet(SetForFixedDivider.Key, s => s.FromSource(new SetForFixedDivider()));
        var tolk = GetTolkForTranslationSet(SetForFixedDivider.Key);

        var result = tolk.Translate("MyFixedDividerKey");

        result.Should().Be("MyFixedDividerValue");
    }
}
