using TechTolk.Registration;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources;

public interface ITranslationSetSource
{
    Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration);
}
