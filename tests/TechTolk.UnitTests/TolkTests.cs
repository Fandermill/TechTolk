using TechTolk.Translations;

namespace TechTolk.UnitTests;

public class TolkTests
{
    [Fact]
    public void Translation_key_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = CreateTolk();

        var result = tolk.Translate(key);

        Assert.Equal("Translated text", result);
    }

    [Fact]
    public void Translation_with_divider_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = CreateTolk();

        var result = tolk.Translate(new FixedStringDivider("nl"), key);

        Assert.Equal("Translated text", result);
    }

    [Fact]
    public void Translation_with_divider_and_data_results_in_string()
    {
        var key = "TRANSLATION_KEY";
        var tolk = CreateTolk();

        var result = tolk.Translate(
            new FixedStringDivider("nl"), 
            key,
            new { name = "Mike" });

        Assert.Equal("Translated text", result);
    }

    private Tolk<string> CreateTolk()
    {
        var translationSet = new TranslationSet<string>("Testing set");
        var nlDictionary = new TranslationDictionary<string>("Test dictionary");
        nlDictionary.Add("TRANSLATION_KEY", new Translation<string>("Translated text"));
        translationSet.AddDivision("nl", nlDictionary);

        var tolk = new Tolk<string>(
            new FixedDividerProvider(new FixedStringDivider("nl")),
            translationSet);

        return tolk;
    }
}