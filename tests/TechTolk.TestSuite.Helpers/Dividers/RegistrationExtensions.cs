using TechTolk.Registration.Builders;

namespace TechTolk.TestSuite.Helpers.Dividers;

public static class RegistrationExtensions
{
    public static ITechTolkBuilder ConfigureDefaultCultureInfoDividers(this ITechTolkBuilder techTolkBuilder)
        => techTolkBuilder.ConfigureDividers(config => config
            .AddSupportedDivider(Constants.CultureInfoDividers.nl_NL)
            .AddSupportedDivider(Constants.CultureInfoDividers.en_US));
}