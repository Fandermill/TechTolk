using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolkTests.Helpers;

namespace TechTolkTests.TestTranslationSets;

public class TranslationSetSourceSpy : ITranslationSetSource
{
    public string Key => nameof(TranslationSetSourceSpy);

    public int NumberOfTimesPopulatedTranslations { get; private set; } = 0;

    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        NumberOfTimesPopulatedTranslations++;

        builder
            .ForDivider(DividerConstants.NL).Add(new[]
            {
                ( "MyKey", "MyValue" + NumberOfTimesPopulatedTranslations)
            });
    }
}