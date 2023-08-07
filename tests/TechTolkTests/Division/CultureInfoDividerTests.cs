using System.Globalization;
using TechTolk.Division;

namespace TechTolkTests.Division;

public class CultureInfoDividerTests
{
    [Fact]
    public void The_divider_key_should_be_the_language_code_of_a_culture_info()
    {
        var cultureInfo = new CultureInfo("nl-NL");
        var divider = new CultureInfoDivider(cultureInfo);

        divider.Key.Should().Be("nl");
    }
}