using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolkTests.Helpers;

namespace TechTolkTests.TestTranslationSets;

public class Set1 : ITranslationSetSource
{
    public const string Key = nameof(Set1);

    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(DividerConstants.NL).Add(new[] {
                ( "MyKey", "NL-MyValue"),
                ( "KeyThatExistsInAllDividers", "NL-ValueFromKeyThatExistsInAllDividers" )
            })
            .ThenForDivider(DividerConstants.EN).Add(new[] {
                ( "MyKey", "EN-MyValue"),
                ( "KeyThatExistsInAllDividers", "EN-ValueFromKeyThatExistsInAllDividers" ),
            });
    }
}

public class Set2 : ITranslationSetSource
{
    public const string Key = nameof(Set2);

    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(DividerConstants.NL).Add(new[] {
                ( "MyKey", "NL-MyValue-FromSet2"),
                ( "AdditionalKey", "NL-AdditionalValue" )
            });
    }
}