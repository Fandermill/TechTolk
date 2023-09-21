using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolkTests.Helpers;

namespace TechTolkTests.TestTranslationSets;

public class SetWithPlaceholderValues : ITranslationSetSource
{
    public const string Key = nameof(SetWithPlaceholderValues);

    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(DividerConstants.NL).Add("KeyWithPlaceholderValues", "Goedemiddag {Name} uit {City}");
        builder.ForDivider(DividerConstants.EN).Add("KeyWithPlaceholderValues", "Good afternoon {Name} from {City}");
    }
}
