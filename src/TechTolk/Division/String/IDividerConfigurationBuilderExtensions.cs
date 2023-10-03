using TechTolk.Division.String;
using TechTolk.Registration.Builders;

namespace TechTolk;

public static class DividerConfigurationBuilderExtensions
{
    public static IDividerConfigurationBuilder AddSupportedDivider(this IDividerConfigurationBuilder builder, string divider)
    {
        return builder.AddSupportedDivider(new StringDivider(divider));
    }
}
