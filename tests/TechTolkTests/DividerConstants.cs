using TechTolk.Registration.Builders;
using TechTolkTests.Helpers;

namespace TechTolkTests;

internal static class DividerConstants
{
    public static readonly StringDivider NL = new(nameof(NL).ToLower());
    public static readonly StringDivider EN = new(nameof(EN).ToLower());

    public static ITechTolkBuilder ConfigureDefaultDividers(this ITechTolkBuilder techTolkBuilder)
        => techTolkBuilder.ConfigureDividers(config => config
            .AddSupportedDivider(NL)
            .AddSupportedDivider(EN));
}