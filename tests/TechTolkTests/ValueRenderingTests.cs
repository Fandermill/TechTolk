using Microsoft.Extensions.DependencyInjection;
using TechTolk;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class ValueRenderingTests : AbstractTechTolkTests
{
    [Fact]
    public void Tolk_uses_custom_value_renderer_if_configured_so()
    {
        _services.AddSingleton(new PrefixingValueRenderer("MyPrefix"));

        _services.AddTechTolk(config =>
        {
            config.UseValueRenderer<PrefixingValueRenderer>();
        }).ConfigureDefaultDividers()
        .AddTranslationSet(Set1.Key, s => s.FromSource(new Set1()));
        var tolk = GetTolkForTranslationSet(Set1.Key);

        var result = tolk.Translate(DividerConstants.NL, "MyKey");

        result.Should().StartWith("MyPrefix");
    }
}
