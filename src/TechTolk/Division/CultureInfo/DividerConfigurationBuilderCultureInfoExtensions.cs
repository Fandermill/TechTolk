using System.Globalization;
using TechTolk.Division.CultureInfo;
using TechTolk.Registration.Builders;

namespace TechTolk;

public static class DividerConfigurationBuilderCultureInfoExtensions
{
    public static IDividerConfigurationBuilder AddSupportedCultureInfoDivider(
        this IDividerConfigurationBuilder builder, string cultureInfoName)
    {
        return builder.AddSupportedDivider(CultureInfoDivider.FromCulture(cultureInfoName));
    }

    public static IDividerConfigurationBuilder AddSupportedCultureInfoDivider(
        this IDividerConfigurationBuilder builder, CultureInfo cultureInfo)
    {
        return builder.AddSupportedDivider(CultureInfoDivider.FromCulture(cultureInfo));
    }
}
