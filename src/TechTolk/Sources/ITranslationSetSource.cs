using TechTolk.Registration;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources;

public interface ITranslationSetSource
{
    void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration);
}
