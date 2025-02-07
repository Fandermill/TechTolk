using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolk.Exceptions;
using TechTolk.Tests.Helpers;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests;

public class FactoryTests : AbstractTechTolkTests
{
    [Fact]
    public void Creating_a_tolk_by_a_type_parameter_uses_correct_translation_set()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(new Set1()));
        var factory = Provider.GetRequiredService<ITolkFactory>();
        var tolk = factory.Create<SharedResource>();

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Resolving_a_typed_tolk_will_use_the_correct_translation_set()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(new Set1()));
        var tolk = Provider.GetRequiredService<ITolk<SharedResource>>();

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Creating_a_tolk_by_a_translation_set_name_that_does_not_exist_throws_and_exception()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers();
        var factory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => factory.Create("NotExistingKey");

        act.Should().Throw<TranslationSetNotFoundException>();
    }

    [Fact]
    public void Creating_a_tolk_for_a_translation_set_that_is_not_loaded_will_throw_an_exception_as_configured()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var factory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => factory.Create(Set1.Key);

        act.Should().ThrowExactly<TranslationSetNotLoadedException>();
    }

    [Fact]
    public void Creating_a_tolk_for_a_translation_set_that_is_not_loaded_will_lazy_load_as_configured()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().LazyLoad())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var factory = Provider.GetRequiredService<ITolkFactory>();

        var tolk = factory.Create(Set1.Key);
        tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey").Should().Be("NL-MyValue");
    }
}