using TechTolk.Division;
using TechTolk.Registration.Builders;

namespace TechTolkTests.Helpers;

internal static class DividerConstants
{
    public static readonly CultureInfoDivider NL = CultureInfoDivider.FromCulture("nl-NL");
    public static readonly CultureInfoDivider EN = CultureInfoDivider.FromCulture("en-US");

    public static readonly StringDivider FixedDivider = new(nameof(FixedDivider));

    public static ITechTolkBuilder ConfigureDefaultDividers(this ITechTolkBuilder techTolkBuilder)
        => techTolkBuilder.ConfigureDividers(config => config
            .AddSupportedDivider(NL)
            .AddSupportedDivider(EN));
}