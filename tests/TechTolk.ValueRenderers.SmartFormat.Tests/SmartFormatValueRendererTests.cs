using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SmartFormat;
using SmartFormat.Core.Extensions;
using SmartFormat.Core.Formatting;
using SmartFormat.Core.Settings;
using SmartFormat.Extensions;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using TechTolk.TranslationSets.Building;
using Xunit;

namespace TechTolk.ValueRenderers.SmartFormat.Tests;

public class SmartFormatValueRendererTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_use_smart_format_with_defaults_as_default_value_renderer()
    {
        // Uses default SmartFormat settings
        // .. like CaseSensitivity should be CaseSensitive

        var builder = _services
            .AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .UseSmartFormatValueRenderer()
            .AddTranslationSet(TestingSetByCultureInfoDividers.Key, s =>
            {
                s.FromSource(new TestingSetByCultureInfoDividers());
            });

        var tolk = GetTolkForTranslationSet(TestingSetByCultureInfoDividers.Key);


        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "PluralizationExample",
            new { Count = 0 });
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "PluralizationExample",
            new { Count = 2 });
        var caseSensitiveAction = () => tolk.Translate(Constants.CultureInfoDividers.nl_NL, "PluralizationExample",
            new { count = 0 });

        nlResult.Should().Be("Je hebt geen berichten.");
        enResult.Should().Be("You have multiple messages.");
        caseSensitiveAction.Should().Throw<FormattingException>();
    }

    [Fact]
    public void Can_use_smart_format_with_options_as_default_value_renderer()
    {
        var formatter = new SmartFormatter(new SmartSettings
        {
            // default is CaseSensitive
            CaseSensitivity = CaseSensitivityType.CaseInsensitive
        })
            // Need this source and formatter in order to work
            .AddExtensions(new ReflectionSource())
            .AddExtensions(new DefaultFormatter());

        var builder = _services
            .AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .UseSmartFormatValueRenderer(formatter)
            .AddTranslationSet(TestingSetByCultureInfoDividers.Key, s =>
            {
                s.FromSource(new TestingSetByCultureInfoDividers());
            });

        var tolk = GetTolkForTranslationSet(TestingSetByCultureInfoDividers.Key);


        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "SimpleExample",
            new { name = "Fandermill" });
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "SimpleExample",
            new { name = "Fandermill" });

        nlResult.Should().Be("Hallo Fandermill");
        enResult.Should().Be("Hello Fandermill");
    }

    [Fact]
    public void Can_use_the_smart_format_value_renderer_for_specific_translation_set()
    {
        string defaultRendererSet = TestingSetByCultureInfoDividers.Key + "DefaultRenderer";
        string smartFormatRendererSet = TestingSetByCultureInfoDividers.Key + "SmartFormatRenderer";

        var builder = _services
            .AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            // this one should use the default renderer
            .AddTranslationSet(defaultRendererSet, s => s.FromSource<TestingSetByCultureInfoDividers>())
            // this one uses the smart format renderer
            .AddTranslationSet(smartFormatRendererSet, s =>
            {
                s.FromSource(new TestingSetByCultureInfoDividers());
                s.WithOptions(o => o.UseSmartFormatValueRenderer());
            });

        var tolkWithDefaultRenderer = GetTolkForTranslationSet(defaultRendererSet);
        var tolkWithSmartFormat = GetTolkForTranslationSet(smartFormatRendererSet);

        var defaultRendererResult = tolkWithDefaultRenderer.Translate(
            Constants.CultureInfoDividers.en_US, "PluralizationExample",
            new { Count = 0 });
        var smartFormatResult = tolkWithSmartFormat.Translate(
            Constants.CultureInfoDividers.en_US, "PluralizationExample",
            new { Count = 2 });

        defaultRendererResult.Should().Be("You have {Count:plural:no messages|a new message|multiple messages}.");
        smartFormatResult.Should().Be("You have multiple messages.");
    }
}

internal class TestingSetByCultureInfoDividers : ITranslationSetSource
{
    public const string Key = nameof(TestingSetByCultureInfoDividers);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(Constants.CultureInfoDividers.nl_NL)
            .Add([
                ("SimpleExample", "Hallo {Name}"),
                ("PluralizationExample", "Je hebt {Count:plural:geen berichten|een nieuw bericht|meerdere berichten}."),
                ("Key", "Value"),
                ])
            .ThenForDivider(Constants.CultureInfoDividers.en_US)
            .Add([
                ("SimpleExample", "Hello {Name}"),
                ("PluralizationExample", "You have {Count:plural:no messages|a new message|multiple messages}."),
                ("Key", "Value"),
                ]);

        return Task.CompletedTask;
    }
}