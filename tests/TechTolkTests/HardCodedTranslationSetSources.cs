using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;

namespace TechTolkTests;

public class SimpleInstantiateTranslationSetSourceFactory<T> : ITranslationSetSourceFactory<T> where T : ITranslationSetSource, new()
{
    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        return new T();
    }
}

public class Set1 : ITranslationSetSource
{
    public const string Key = "Set1";

    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(DividerConstants.NL).Add(new[] {
                ( "MyKey", "NL-MyValue"),
                ( "KeyThatExistsInAllDividers", "NL-ValueFromKeyThatExistsInAllDividers" ),
            })
            .ThenForDivider(DividerConstants.EN).Add(new[] {
                ( "MyKey", "EN-MyValue"),
                ( "KeyThatExistsInAllDividers", "EN-ValueFromKeyThatExistsInAllDividers" ),
            });
    }
}