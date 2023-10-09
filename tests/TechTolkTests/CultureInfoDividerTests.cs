using System.Globalization;
using TechTolk.Division;

namespace TechTolkTests;

public class CultureInfoDividerTests
{
    [Fact]
    public void The_divider_key_should_be_the_name_of_a_culture_info()
    {
        var culture = new CultureInfo("nl-NL");
        var divider = CultureInfoDivider.FromCulture(culture);

        divider.Key.Should().Be("nl-NL");
    }
}