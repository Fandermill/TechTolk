using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolk.Exceptions;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class FactoryTests : AbstractTechTolkTests
{
    [Fact]
    public void Creating_a_tolk_by_a_type_parameter_uses_correct_translation_set()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(new Set1()));
        var factory = Provider.GetRequiredService<ITolkFactory>();
        var tolk = factory.Create<SharedResource>();

        var result = tolk.Translate(DividerConstants.NL, "MyKey");
        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Resolving_a_typed_tolk_will_use_the_correct_translation_set()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(new Set1()));
        var tolk = Provider.GetRequiredService<ITolk<SharedResource>>();

        var result = tolk.Translate(DividerConstants.NL, "MyKey");
        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Creating_a_tolk_by_a_translation_set_name_that_does_not_exist_throws_and_exception()
    {
        _services.AddTechTolk().ConfigureDefaultDividers();
        var factory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => factory.Create("NotExistingKey");

        act.Should().Throw<TranslationSetNotFoundException>();
    }

    [Fact]
    public void Creating_a_tolk_for_a_translation_set_that_is_not_loaded_will_throw_an_exception_as_configured()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultDividers()
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
            .ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var factory = Provider.GetRequiredService<ITolkFactory>();

        var tolk = factory.Create(Set1.Key);
        tolk.Translate(DividerConstants.NL, "MyKey").Should().Be("NL-MyValue");
    }
}
