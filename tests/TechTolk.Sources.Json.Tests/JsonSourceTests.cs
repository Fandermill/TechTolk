using FluentAssertions;
using TechTolk.Exceptions;
using TechTolk.Sources.Json.Exceptions;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using Xunit;

namespace TechTolk.Sources.Json.Tests;

public class JsonSourceTests : AbstractTechTolkTests
{
    private const string JSON_DIRECTORY = "./../../../JsonFiles/";

    [Fact]
    public void Can_read_multiple_translation_sets_from_json_file()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "AllInOne.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("AllInOne-MyValue-NL");
        enResult.Should().Be("AllInOne-MyValue-EN");
    }

    [Fact]
    public void When_a_translation_set_is_missing_the_divider_property_in_a_multiple_set_json_file_an_exception_will_be_thrown()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "AllInOne-MissingDivider.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void Can_read_single_translation_set_from_json_files()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "PerDivider.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("PerDivider-MyValue-NL");
    }

    [Fact]
    public void Divider_property_of_single_translation_set_is_not_mandatory_when_filename_contained_divider_key()
    {
        var builder = _services.AddTechTolk()
            .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "PerDivider-MissingDivider.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("PerDivider-MissingDivider-MyValue-NL");
    }

    [Fact]
    public void Can_read_multiple_and_single_translation_sets_mixed_in_one_file()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "Mixed.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("Mixed-MyValue-NL");
        enResult.Should().Be("Mixed-MyValue-EN");
    }

    [Fact]
    public void Can_read_translations_from_an_object_property()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "ReadTranslationsFromObject.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var result = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey2");

        result.Should().Be("MyValue2");
    }

    [Fact]
    public void When_an_expected_property_is_missing_an_exception_will_be_thrown()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "MissingTranslationsProperty.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void When_a_property_is_not_of_the_expected_value_kind_an_exception_will_be_thrown()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "InvalidValueKind.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void When_no_translation_set_properties_are_found_in_a_file_an_exception_will_be_thrown()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "MissingTranslationSetProperties.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void File_paths_with_the_exact_filename_and_filenames_with_supported_dividers_will_all_be_read()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "ValidFilePaths.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("ValidFilePaths-MyValue-NL");
        enResult.Should().Be("ValidFilePaths-MyValue-EN");
    }

    [Fact]
    public void File_paths_with_unsupported_divider_in_filename_will_not_be_read()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "UnsupportedDividerInFilename.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_a_translation_key_occurs_multiple_times_the_value_is_replaced_by_the_last()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "DuplicateKeys.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("DuplicateKeys-MyValue-2-NL");
        enResult.Should().Be("DuplicateKeys-MyValue-2-EN");
    }

    [Fact]
    public void Translation_sets_of_unsupported_dividers_are_ignored()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "UnsupportedDivider.json"));
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<UnsupportedDividerException>();
    }


    [Fact]
    public void A_JSON_file_is_allowed_to_contain_comments()
    {
        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson(Path.Combine(JSON_DIRECTORY, "AllInOne-WithComments.json"));
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("AllInOne-WithComments-NL");
        enResult.Should().Be("AllInOne-WithComments-EN");
    }



    [Fact]
    public void Can_register_a_json_source_through_the_translation_set_extension_method()
    {
        _services.AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSetFromJson(Path.Combine(JSON_DIRECTORY, "AllInOne.json"));

        var tolk = GetTolkForTranslationSet("AllInOne");

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("AllInOne-MyValue-NL");
    }

    [Fact]
    public void Can_register_a_json_source_through_the_translation_set_extension_method_with_a_generic_type_parameter()
    {
        _services.AddTechTolk()
            .ConfigureDefaultCultureInfoDividers()
            .AddTranslationSetFromJson<JsonSourceTests>(Path.Combine(JSON_DIRECTORY, "AllInOne.json"));

        var tolk = GetTolkForTranslationSet<JsonSourceTests>();

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("AllInOne-MyValue-NL");
    }
}
