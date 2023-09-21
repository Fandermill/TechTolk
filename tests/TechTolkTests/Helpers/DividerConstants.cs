using TechTolk.Registration.Builders;

namespace TechTolkTests.Helpers;

internal static class DividerConstants
{
    public static readonly StringDivider NL = new(nameof(NL).ToLower());
    public static readonly StringDivider EN = new(nameof(EN).ToLower());

    public static readonly StringDivider FixedDivider = new(nameof(FixedDivider));

    public static ITechTolkBuilder ConfigureDefaultDividers(this ITechTolkBuilder techTolkBuilder)
        => techTolkBuilder.ConfigureDividers(config => config
            .AddSupportedDivider(NL)
            .AddSupportedDivider(EN));
}