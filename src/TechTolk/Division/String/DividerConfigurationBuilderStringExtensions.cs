using TechTolk.Division.String;
using TechTolk.Registration.Builders;

namespace TechTolk;

public static class DividerConfigurationBuilderStringExtensions
{
    public static IDividerConfigurationBuilder AddSupportedStringDivider(this IDividerConfigurationBuilder builder, string divider)
    {
        return builder.AddSupportedDivider(new StringDivider(divider));
    }
}
