using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TestSuite.Helpers.Dividers;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Tests.TestTranslationSets;

public class TranslationSetSourceSpy : ITranslationSetSource
{
    public string Key => nameof(TranslationSetSourceSpy);

    public int NumberOfTimesPopulatedTranslations { get; private set; } = 0;

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        NumberOfTimesPopulatedTranslations++;

        builder
            .ForDivider(Constants.CultureInfoDividers.nl_NL).Add(new[]
            {
                ( "MyKey", "MyValue" + NumberOfTimesPopulatedTranslations)
            });

        return Task.CompletedTask;
    }
}