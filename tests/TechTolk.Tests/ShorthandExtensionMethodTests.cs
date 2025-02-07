using TechTolk.Registration.Builders;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;

namespace TechTolk.Tests;

public class ShorthandExtensionMethodTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_use_the_shorthand_extension_methods_as_the_normal_methods_were_used()
    {
        _services
            .AddTechTolk()
            .UseCultureInfoDividers("nl-NL", "en-US")
            .AddTranslationSetFromSource<Set1>();

        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("nl-NL");
        var tolk = GetTolkForTranslationSet<Set1>();

        var result = tolk.Translate("MyKey");

        result.Should().Be("NL-MyValue");
    }
}