using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolk.Exceptions;
using TechTolk.Sources;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class SourceTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_register_a_source_from_a_source_factory()
    {
        _services.AddSingleton(typeof(ITranslationSetSourceFactory<>), typeof(SimpleInstantiateTranslationSetSourceFactory<>));
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource<Set1>());
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().NotThrow();
    }

    [Fact]
    public void Adding_a_translation_set_source_by_type_without_registering_a_factory_in_di_throws_an_exception()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource<Set1>());
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().Throw<RegistrationException>();
    }

    [Fact]
    public void Adding_a_translation_set_without_sources_throws_an_exception()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(Set1.Key, s => { });
        var tolkFactory = Provider.GetRequiredService<ITolkFactory>();

        var act = () => tolkFactory.Create(Set1.Key);

        act.Should().Throw<TechTolkException>();
    }
}
