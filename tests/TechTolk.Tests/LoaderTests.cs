using Microsoft.Extensions.DependencyInjection;
using TechTolk.Exceptions;
using TechTolk.Tests.Helpers;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests;

public class LoaderTests : AbstractTechTolkTests
{
    [Fact]
    public async Task Loading_a_translation_set_with_an_existing_registration_will_succeed()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var sut = Provider.GetRequiredService<ITolkLoader>();
        await sut.LoadTranslationSetAsync(Set1.Key);

        var tolk = GetTolkForTranslationSet(Set1.Key);

        tolk
            .Translate(Constants.CultureInfoDividers.nl_NL, "KeyThatExistsInAllDividers")
            .Should().Be("NL-ValueFromKeyThatExistsInAllDividers");
    }

    [Fact]
    public async Task Loading_a_translation_set_that_is_already_loaded_will_do_nothing()
    {
        var translationSetSource = new TranslationSetSourceSpy();

        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(translationSetSource.Key, s => s.FromSource(translationSetSource));
        var sut = Provider.GetRequiredService<ITolkLoader>();

        await sut.LoadTranslationSetAsync(translationSetSource.Key);
        var tolk1 = GetTolkForTranslationSet(translationSetSource.Key);
        var val1 = tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        await sut.LoadTranslationSetAsync(translationSetSource.Key);
        var tolk2 = GetTolkForTranslationSet(translationSetSource.Key);
        var val2 = tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        translationSetSource.NumberOfTimesPopulatedTranslations.Should().Be(1);
        val2.Should().Be(val1);
    }

    [Fact]
    public async Task Loading_a_translation_set_without_an_existing_registration_will_fail()
    {
        _services
            .AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var sut = Provider.GetRequiredService<ITolkLoader>();

        var act = async () => await sut.LoadTranslationSetAsync("MyKey");

        await act.Should().ThrowExactlyAsync<TranslationSetNotFoundException>();
    }

    [Fact]
    public async Task Reloading_a_translation_set_will_recreate_the_translation_set_even_when_it_was_already_loaded()
    {
        var translationSetSource = new TranslationSetSourceSpy();

        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(translationSetSource.Key, s => s.FromSource(translationSetSource));
        var sut = Provider.GetRequiredService<ITolkLoader>();

        await sut.LoadTranslationSetAsync(translationSetSource.Key);
        var tolk1 = GetTolkForTranslationSet(translationSetSource.Key);
        var val1 = tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        await sut.ReloadTranslationSetAsync(translationSetSource.Key);
        var tolk2 = GetTolkForTranslationSet(translationSetSource.Key);
        var val2 = tolk2.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        translationSetSource.NumberOfTimesPopulatedTranslations.Should().Be(2);
        val2.Should().NotBe(val1);
    }

    [Fact]
    public async Task Injecting_an_ITolk_after_reloading_a_translation_set_returns_new_values()
    {
        var translationSetSource = new TranslationSetSourceSpy();

        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(translationSetSource));
        var sut = Provider.GetRequiredService<ITolkLoader>();

        await sut.LoadTranslationSetAsync<SharedResource>();
        var tolk1 = Provider.GetRequiredService<ITolk<SharedResource>>();
        var val1 = tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        await sut.ReloadTranslationSetAsync<SharedResource>();
        var tolk2 = Provider.GetRequiredService<ITolk<SharedResource>>();
        var val2 = tolk2.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        val1.Should().Be("MyValue1");
        val2.Should().Be("MyValue2");
    }

    [Fact]
    public async Task Clearing_a_translation_set_will_remove_an_existing_translation_set()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var loader = Provider.GetRequiredService<ITolkLoader>();
        await loader.LoadTranslationSetAsync(Set1.Key);
        var tolk1 = GetTolkForTranslationSet(Set1.Key);

        loader.ClearTranslationSet(Set1.Key);

        var act = () => Provider.GetRequiredService<ITolkFactory>().Create(Set1.Key);

        act.Should().ThrowExactly<TranslationSetNotLoadedException>();

        // tolk1 still owns a reference to the translation set that is cleared later,
        // showing that the translation set was indeed loaded before
        tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey").Should().Be("NL-MyValue");
    }

    [Fact]
    public void Clearing_a_translation_set_that_does_not_exist_does_nothing()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));

        var loader = Provider.GetRequiredService<ITolkLoader>();
        loader.ClearTranslationSet(Set1.Key);
        loader.ClearTranslationSet(Set1.Key);

        var act = () => Provider.GetRequiredService<ITolkFactory>().Create(Set1.Key);
        act.Should().ThrowExactly<TranslationSetNotLoadedException>();
    }

    [Fact]
    public async Task Clearing_all_translation_sets_will_unload_all_loaded_translation_sets()
    {
        _services
            .AddTechTolk(options => options.OnTranslationSetNotLoaded().ThrowException())
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()))
            .AddTranslationSet(Set2.Key, s => s.FromSource(new Set2()));
        var loader = Provider.GetRequiredService<ITolkLoader>();
        await loader.LoadTranslationSetAsync(Set1.Key);
        await loader.LoadTranslationSetAsync(Set2.Key);
        var factory = Provider.GetRequiredService<ITolkFactory>();
        var set1Tolk1 = factory.Create(Set1.Key);
        var set2Tolk1 = factory.Create(Set2.Key);

        loader.ClearAllTranslationSets();

        var actSet1 = () => factory.Create(Set1.Key);
        var actSet2 = () => factory.Create(Set2.Key);

        actSet1.Should().ThrowExactly<TranslationSetNotLoadedException>();
        actSet2.Should().ThrowExactly<TranslationSetNotLoadedException>();

        // these tolks still own a reference to a translation set that is cleared later,
        // showing that the translation sets were indeed loaded before
        set1Tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey").Should().Be("NL-MyValue");
        set2Tolk1.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey").Should().Be("NL-MyValue-FromSet2");
    }
}