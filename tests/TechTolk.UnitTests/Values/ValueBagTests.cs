using TechTolk.Values;

namespace TechTolk.UnitTests.Values;

public class ValueBagTests
{
    [Fact]
    public void Can_set_value_to_value_bag()
    {
        var bag = new ValueBag();

        bag.Set("TestKey1", "TestValue1");
        bag["TestKey2"] = "TestValue2";


        Assert.Equal("TestValue1", bag.Get("TestKey1"));
        Assert.Equal("TestValue2", bag["TestKey2"]);
    }

    [Fact]
    public void Setting_existing_key_overwrites_previous_value()
    {
        var bag = new ValueBag();
        bag.Set("TestKey", "FirstValue");
        
        bag.Set("TestKey", "NewValue");

        Assert.Equal("NewValue", bag.Get("TestKey"));
    }

    [Fact]
    public void Can_get_value_by_key()
    {
        var bag = new ValueBag();
        bag.Set("TestKey", "TestValue");

        var result = bag["TestKey"];

        Assert.Equal("TestValue", result);
    }

    [Fact]
    public void Getting_key_that_does_not_exist_returns_null()
    {
        var bag = new ValueBag();

        var result = bag["NotExistingKey"];

        Assert.Null(result);
    }
}