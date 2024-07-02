using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
    public void Can_user_smart_format()
    {
        _services.AddSingleton<SmartFormatValueRenderer>();

        var builder = _services
            .AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .UseSmartFormatValueRenderer(_services)
            .AddTranslationSet(TestingSetByCultureInfoDividers.Key, s =>
            {
                s.FromSource(new TestingSetByCultureInfoDividers());
            });

        var tolk = GetTolkForTranslationSet(TestingSetByCultureInfoDividers.Key);


        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "PluralizationExample",
            new { Name = "Fandermill", Count = 0 });
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "PluralizationExample",
            new { Name = "Fandermill", Count = 2 });

        nlResult.Should().Be("Hallo Fandermill, je hebt geen berichten.");
        enResult.Should().Be("Hello Fandermill, you have multiple messages.");
    }
}

internal class TestingSetByCultureInfoDividers : ITranslationSetSource
{
    public const string Key = nameof(TestingSetByCultureInfoDividers);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(Constants.CultureInfoDividers.nl_NL)
            .Add([
                ("PluralizationExample", "Hallo {Name}, je hebt {Count:plural:geen berichten|een nieuw bericht|meerdere berichten}."),
                ("Key", "Value"),
                ])
            .ThenForDivider(Constants.CultureInfoDividers.en_US)
            .Add([
                ("PluralizationExample", "Hello {Name}, you have {Count:plural:no messages|a new message|multiple messages}."),
                ("Key", "Value"),
                ]);

        return Task.CompletedTask;
    }
}