using FluentAssertions;
using TechTolk.Tests.Shared;

namespace TechTolk.IntegrationTests;

public class TolkTests
{
    [Fact]
    public void Can_get_translation_value_for_key_without_divider()
    {
        var tolk = CreateTolk();
        var key = "TEST_KEY";

        var result = tolk.Translate(key);

        result.Should().Be("Translation Value");
    }

    [Fact]
    public void Can_get_translation_value_for_key_with_divider()
    {
        var tolk = CreateTolk();
        var key = "TEST_KEY";

        var result = tolk.For(new FixedStringDivider("divider")).Translate(key);

        result.Should().Be("Translation Value");
    }

    [Fact]
    public void Can_get_translation_value_for_key_and_data()
    {
        var tolk = CreateTolk();
        var key = "TEST_KEY";

        var result = tolk.Translate(key, new { myData = "myDataValue" });

        result.Should().Be("Translation Value with data: myDataValue");
    }

    [Fact]
    public void Not_existing_key_returns_null()
    {
        var tolk = CreateTolk();
        var key = "NOT_EXISTING_KEY";

        var result = tolk.Translate(key);

        result.Should().BeNull();
    }

    [Fact]
    public void Not_existing_key_returns_key_itself()
    {
        var tolk = CreateTolk();
        var key = "NOT_EXISTING_KEY";

        var result = tolk.Translate(key);

        result.Should().Be(key);
    }



    private ITolk<string> CreateTolk()
    {
        throw new NotImplementedException();
    }
}
