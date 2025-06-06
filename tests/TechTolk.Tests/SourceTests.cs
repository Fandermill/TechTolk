using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolk.Exceptions;
using TechTolk.Sources;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests;

public class SourceTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_register_a_source_from_a_source_factory()
    {
        _services.AddSingleton(typeof(ITranslationSetSourceFactory<>), typeof(SimpleInstantiateTranslationSetSourceFactory<>));
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource<Set1>());
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().NotThrow();
    }

    [Fact]
    public void Adding_a_translation_set_source_by_type_with_complex_constructor_without_registering_a_factory_in_di_throws_an_exception()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(SetWithComplexConstructor.Key, s => s.FromSource<SetWithComplexConstructor>());
        var loader = Provider.GetRequiredService<ITolkLoader>();

        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(SetWithComplexConstructor.Key);

        act.Should().Throw<RegistrationException>();
    }

    [Fact]
    public void Adding_a_translation_set_source_by_type_without_registering_a_factory_but_source_has_a_parameterless_constructor_is_ok()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource<Set1>());
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().NotThrow();
    }

    [Fact]
    public void Adding_a_translation_set_without_sources_throws_an_exception()
    {
        _services.AddTechTolk().ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => { });
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().Throw<TechTolkException>();
    }
}