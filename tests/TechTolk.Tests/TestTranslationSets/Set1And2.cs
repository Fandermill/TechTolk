using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolk.Tests.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests.TestTranslationSets;

public class Set1 : ITranslationSetSource
{
    public const string Key = nameof(Set1);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(Constants.CultureInfoDividers.nl_NL).Add(new[] {
                ( "MyKey", "NL-MyValue"),
                ( "KeyThatExistsInAllDividers", "NL-ValueFromKeyThatExistsInAllDividers" )
            })
            .ThenForDivider(Constants.CultureInfoDividers.en_US).Add(new[] {
                ( "MyKey", "EN-MyValue"),
                ( "KeyThatExistsInAllDividers", "EN-ValueFromKeyThatExistsInAllDividers" ),
            });

        return Task.CompletedTask;
    }
}

public class Set2 : ITranslationSetSource
{
    public const string Key = nameof(Set2);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(Constants.CultureInfoDividers.nl_NL).Add(new[] {
                ( "MyKey", "NL-MyValue-FromSet2"),
                ( "AdditionalKey", "NL-AdditionalValue" )
            });

        return Task.CompletedTask;
    }
}

public class SetWithComplexConstructor : ITranslationSetSource
{
    public const string Key = nameof(SetWithComplexConstructor);

    public SetWithComplexConstructor(string notParameterlessCtor)
    {
        // the parameter in this constructor is on purpose for testing
    }

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(Constants.CultureInfoDividers.nl_NL).Add(new[] {
                ( "MyKey", "MyValue")
            });

        return Task.CompletedTask;
    }
}