using TechTolk.Registration;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources.Resx;

internal abstract class ResxTranslationSetSource : ITranslationSetSource
{
    public async Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        var options = sourceRegistration.GetOptions<ResxTranslationSetSourceOptions>();
        await PopulateTranslationsAsync(builder, options);
    }

    protected abstract Task PopulateTranslationsAsync(ITranslationSetBuilder builder, ResxTranslationSetSourceOptions options);
}



