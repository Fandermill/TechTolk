using Microsoft.Extensions.DependencyInjection;
using TechTolk.Tests.Helpers;
using TechTolk.Tests.TestTranslationSets;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests;

public class ValueRenderingTests : AbstractTechTolkTests
{
    [Fact]
    public void Tolk_uses_custom_value_renderer_if_configured_so()
    {
        _services.AddSingleton(new PrefixingValueRenderer("MyPrefix"));

        _services.AddTechTolk(config =>
        {
            config.UseValueRenderer<PrefixingValueRenderer>();
        }).ConfigureDefaultCultureInfoDividers()
        .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().StartWith("MyPrefix");
    }
}