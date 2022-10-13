using FluentAssertions;
using TechTolk.Values;

namespace TechTolk.UnitTests.Values;

public class ValueBagConverterTests
{
    [Fact]
    public void Can_convert_anonymous_object_into_value_bag()
    {
        var obj = new { date = DateTime.Now, name = "My name" };
        var converter = new ValueBagConverter();

        var valueBag = converter.ConvertFromObject(obj);

        valueBag["date"].Should().Be(obj.date);
        valueBag["name"].Should().Be(obj.name);
    }
}
