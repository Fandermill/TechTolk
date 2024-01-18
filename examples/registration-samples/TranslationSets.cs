using TechTolk.Division.CultureInfo;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;

namespace registration_samples;

public sealed class HardCodedSetA : ITranslationSetSource
{
    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder

            .ForDivider(CultureInfoDivider.FromCulture("nl-NL"))
            .Add(new[]
            {
                ("SetA-TranslationKey", "My Dutch Value from set A"),
                ("KeyOnlyForNL", "Dutch value for key that only exists for NL"),
                ("KeyInAllSets", "Dutch value from set A for key that exists in all sets")
            })

            .ThenForDivider(CultureInfoDivider.FromCulture("en-US"))
            .Add(new[] {
                ("SetA-TranslationKey", "My English Value from set A"),
                ("KeyInAllSets", "English value from set A for key that exists in all sets")
            });
    }
}

public sealed class HardCodedSetB : ITranslationSetSource
{
    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder

            .ForDivider(CultureInfoDivider.FromCulture("nl-NL"))
            .Add(new[]
            {
                ("SetB-TranslationKey", "My Dutch Value from set B"),
                ("KeyInAllSets", "Dutch value from set B for key that exists in all sets")
            })

            .ThenForDivider(CultureInfoDivider.FromCulture("en-US"))
            .Add(new[] {
                ("SetB-TranslationKey", "My English Value from set B"),
                ("KeyInAllSets", "English value from set B for key that exists in all sets")
            });
    }
}
