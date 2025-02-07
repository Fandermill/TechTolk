using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Tests.TestTranslationSets;

public class SetForFixedDivider : ITranslationSetSource
{
    public const string Key = nameof(SetForFixedDivider);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(Constants.StringDividers.FixedDivider).Add("MyFixedDividerKey", "MyFixedDividerValue");
        return Task.CompletedTask;
    }
}