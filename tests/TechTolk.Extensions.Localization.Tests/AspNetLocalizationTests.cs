using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using TechTolk.Exceptions;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Extensions.Localization.Tests;

public class AspNetLocalizationTests : AbstractTechTolkTests
{
    [Fact]
    public void Using_the_aspnet_localization_string_localizer_of_t_will_return_translations_loaded_by_tech_tolk()
    {
        _services.AddLocalization();

        _services
            .AddTechTolk()
            .ConfigureDefaultDividers()
            .AddTranslationSet<SharedResource>(s => s.FromSource(new SharedResourceSet()));
        _services.AddTechTolkAspNetLocalization();

        var localizer = Provider.GetRequiredService<IStringLocalizer<SharedResource>>();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        var result = localizer.GetString("MyKey");

        result.ResourceNotFound.Should().BeFalse();
        result.Value.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Using_the_aspnet_localization_string_localizer_factory_will_return_translations_loaded_by_tech_tolk()
    {
        _services.AddLocalization();

        _services
            .AddTechTolk()
            .ConfigureDefaultDividers()
            .AddTranslationSet<SharedResourceSet>(s => s.FromSource(new SharedResourceSet()));
        _services.AddTechTolkAspNetLocalization();

        var localizerFactory = Provider.GetRequiredService<IStringLocalizerFactory>();
        var localizer = localizerFactory.Create(typeof(SharedResourceSet));

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        var result = localizer.GetString("MyKey");

        result.ResourceNotFound.Should().BeFalse();
        result.Value.Should().Be("NL-MyValue");
    }

    [Theory]
    [InlineData("Set", "Set", null)]
    [InlineData("Set", "Set", "")]
    [InlineData("Set", "Set", " ")]
    [InlineData("Prefix.Set", "Set", "Prefix")]
    public void Using_the_string_localizer_factory_will_prepend_basename_with_location_which_matches_a_translation_set_name(
        string actualSetName, string baseName, string? location)
    {
        _services.AddLocalization();

        _services
            .AddTechTolk()
            .ConfigureDefaultDividers()
            .AddTranslationSet(actualSetName, (s) => s.FromSource(new SharedResourceSet()));
        _services.AddTechTolkAspNetLocalization();

        var localizerFactory = Provider.GetRequiredService<IStringLocalizerFactory>();
        var localizer = localizerFactory.Create(baseName, location!);

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        var result = localizer.GetString("MyKey");

        result.ResourceNotFound.Should().BeFalse();
        result.Value.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Requesting_a_string_localizer_of_t_that_is_not_registered_with_tech_tolk_should_throw_an_exception()
    {
        _services.AddLocalization();

        _services
            .AddTechTolk()
            .ConfigureDefaultDividers();
        _services.AddTechTolkAspNetLocalization();


        var act = () => Provider.GetRequiredService<IStringLocalizer<SharedResource>>();


        act.Should().Throw<TranslationSetNotFoundException>();
    }

    private class SharedResource { }

    private class SharedResourceSet : ITranslationSetSource
    {
        public const string Key = nameof(SharedResourceSet);

        public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
        {
            builder
                .ForDivider(Constants.CultureInfoDividers.nl_NL).Add(new[] {
                    ( "MyKey", "NL-MyValue")
                })
                .ThenForDivider(Constants.CultureInfoDividers.en_US).Add(new[] {
                    ( "MyKey", "EN-MyValue")
                });

            return Task.CompletedTask;
        }
    }
}
