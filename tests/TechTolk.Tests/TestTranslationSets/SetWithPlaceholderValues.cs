using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolk.Tests.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;

namespace TechTolk.Tests.TestTranslationSets;

public class SetWithPlaceholderValues : ITranslationSetSource
{
    public const string Key = nameof(SetWithPlaceholderValues);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(Constants.CultureInfoDividers.nl_NL).Add("KeyWithPlaceholderValues", "Goedemiddag {Name} uit {City}");
        builder.ForDivider(Constants.CultureInfoDividers.en_US).Add("KeyWithPlaceholderValues", "Good afternoon {Name} from {City}");
        return Task.CompletedTask;
    }
}
