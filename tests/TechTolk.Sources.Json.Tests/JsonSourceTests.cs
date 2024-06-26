﻿using FluentAssertions;
using TechTolk.Exceptions;
using TechTolk.Registration.Builders;
using TechTolk.Sources.Json.Exceptions;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using Xunit;

namespace TechTolk.Sources.Json.Tests;

public class JsonSourceTests : AbstractTechTolkTests
{
    [Fact]
    public void Can_read_multiple_translation_sets_from_json_file()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/AllInOne.json");
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
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/AllInOne-MissingDivider.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void Can_read_single_translation_set_from_json_files()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/PerDivider.json");
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("PerDivider-MyValue-NL");
    }

    [Fact]
    public void Divider_property_of_single_translation_set_is_not_mandatory_when_filename_contained_divider_key()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/PerDivider-MissingDivider.json");
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

        result.Should().Be("PerDivider-MissingDivider-MyValue-NL");
    }

    [Fact]
    public void Can_read_multiple_and_single_translation_sets_mixed_in_one_file()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/Mixed.json");
        });

        var tolk = GetTolkForTranslationSet("Set1");

        var nlResult = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");
        var enResult = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

        nlResult.Should().Be("Mixed-MyValue-NL");
        enResult.Should().Be("Mixed-MyValue-EN");
    }

    [Fact]
    public void When_an_expected_property_is_missing_an_exception_will_be_thrown()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/MissingTranslationsProperty.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void When_a_property_is_not_of_the_expected_value_kind_an_exception_will_be_thrown()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/InvalidValueKind.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void When_no_translation_set_properties_are_found_in_a_file_an_exception_will_be_thrown()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/MissingTranslationSetProperties.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<JsonFormatException>();
    }

    [Fact]
    public void File_paths_with_the_exact_filename_and_filenames_with_supported_dividers_will_all_be_read()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/ValidFilePaths.json");
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
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/UnsupportedDividerInFilename.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void When_a_translation_key_occurs_multiple_times_the_value_is_replaced_by_the_last()
    {
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/DuplicateKeys.json");
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
        _services.AddTechTolkJsonServices();

        var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

        builder.AddTranslationSet("Set1", set =>
        {
            set.FromJson("./JsonFiles/UnsupportedDivider.json");
        });

        var act = () => GetTolkForTranslationSet("Set1");

        act.Should().Throw<UnsupportedDividerException>();
    }
}
