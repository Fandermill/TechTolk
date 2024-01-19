using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;
using TechTolkTests.Helpers;

namespace TechTolkTests.TestTranslationSets;

public class SetForFixedDivider : ITranslationSetSource
{
    public const string Key = nameof(SetForFixedDivider);

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(DividerConstants.FixedDivider).Add("MyFixedDividerKey", "MyFixedDividerValue");
        return Task.CompletedTask;
    }
}