using System.Globalization;
using TechTolk.Registration.Builders;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;

namespace TechTolk.Tests;

public class ShorthandExtensionMethodTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_use_the_shorthand_extension_methods_for_strings_as_if_the_normal_methods_were_used()
    {
        _services
            .AddTechTolk()
            .UseCultureInfoDividers("nl-NL", "en-US")
            .AddTranslationSetFromSource<Set1>();

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");
        var tolk = GetTolkForTranslationSet<Set1>();

        var result = tolk.Translate("MyKey");

        result.Should().Be("NL-MyValue");
    }

    [Fact]
    public void Can_use_the_shorthand_extension_methods_for_culture_infos_as_if_the_normal_methods_were_used()
    {
        var supportedCultures = new[]
        {
            new CultureInfo("nl-NL"),
            new CultureInfo("en-US")
        };

        _services
            .AddTechTolk()
            .UseCultureInfoDividers(supportedCultures)
            .AddTranslationSetFromSource<Set1>();

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        var tolk = GetTolkForTranslationSet<Set1>();

        var result = tolk.Translate("MyKey");

        result.Should().Be("EN-MyValue");
    }
}