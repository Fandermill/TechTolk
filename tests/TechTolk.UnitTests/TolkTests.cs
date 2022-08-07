namespace TechTolk.UnitTests;

public class TolkTests
{
    [Fact]
    public void Translation_key_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = new Tolk(new FixedDividerProvider(new FixedStringDivider("Divider")));

        var result = tolk.Translate(key);

        Assert.Equal("Translated text", result);
    }

    [Fact]
    public void Translation_with_divider_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = new Tolk(new FixedDividerProvider(new FixedStringDivider("Divider")));

        var result = tolk.Translate(new FixedStringDivider("Divider"), key);

        Assert.Equal("Translated text", result);
    }

    [Fact]
    public void Translation_with_divider_and_data_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = new Tolk(new FixedDividerProvider(new FixedStringDivider("Divider")));

        var result = tolk.Translate(
            new FixedStringDivider("Divider"), 
            key,
            new { name = "Mike" });

        Assert.Equal("Translated text", result);
    }
}