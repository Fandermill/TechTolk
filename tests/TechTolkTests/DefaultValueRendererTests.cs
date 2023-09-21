using TechTolk;
using TechTolkTests.Helpers;
using TechTolkTests.TestTranslationSets;

namespace TechTolkTests;

public class DefaultValueRendererTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_render_values_using_an_anonymous_object_as_data_parameter()
    {
        var tolk = SetupWithDefaultValueRenderer();
        var data = new { Name = "Jim", City = "Amsterdam" };

        var result = tolk.Translate(DividerConstants.NL, "KeyWithPlaceholderValues", data);

        result.Should().Be("Goedemiddag Jim uit Amsterdam");
    }

    [Fact]
    public void Can_render_values_using_a_dictionary_as_data_parameter()
    {
        var tolk = SetupWithDefaultValueRenderer();
        var data = new Dictionary<string, object>
        {
            { "Name", "Jim" },
            { "City", "Amsterdam" }
        };

        var result = tolk.Translate(DividerConstants.NL, "KeyWithPlaceholderValues", data);

        result.Should().Be("Goedemiddag Jim uit Amsterdam");
    }

    [Fact]
    public void Can_render_values_using_an_object_array_as_data_parameter()
    {
        SetupWithDefaultValueRenderer();
        var tolk = GetTolkForTranslationSet(SetWithPlaceholderValues.Key);
        var data = new object[] { "Jim", "Amsterdam" };

        var result = tolk.Translate(DividerConstants.NL, "KeyWithPlaceholderValues", data);

        result.Should().Be("Goedemiddag Jim uit Amsterdam");
    }

    [Fact]
    public void Missing_value_for_placeholder_keeps_placeholder_in_tact()
    {
        var tolk = SetupWithDefaultValueRenderer();
        var data = new { Name = "Jim" };

        var result = tolk.Translate(DividerConstants.NL, "KeyWithPlaceholderValues", data);

        result.Should().Be("Goedemiddag Jim uit {City}");
    }



    private ITolk SetupWithDefaultValueRenderer()
    {
        _services.AddTechTolk().ConfigureDefaultDividers()
            .AddTranslationSet(SetWithPlaceholderValues.Key, s => s.FromSource(new SetWithPlaceholderValues()));
        return GetTolkForTranslationSet(SetWithPlaceholderValues.Key);
    }
}
