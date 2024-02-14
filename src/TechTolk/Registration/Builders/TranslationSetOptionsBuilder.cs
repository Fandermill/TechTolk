using TechTolk.Rendering;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal class TranslationSetOptionsBuilder : ITranslationSetOptionsBuilder
{
    private readonly TranslationSetOptions _options;

    public TranslationSetOptionsBuilder(TranslationSetOptions options)
    {
        _options = options;
    }

    public ITranslationSetOptionsBuilder UseValueRenderer<T>() where T : AbstractTranslationValueRenderer
    {
        _options.ValueRendererType = typeof(T);
        return this;
    }

    public ITranslationNotFoundBehaviorConfigurationBuilder OnTranslationNotFound()
    {
        return new TranslationNotFoundBehaviorConfigurationBuilder(this, _options);
    }

    public ITranslationSetNotLoadedBehaviorConfigurationBuilder OnTranslationSetNotLoaded()
    {
        return new TranslationSetNotLoadedBehaviorConfigurationBuilder(this, _options);
    }
}
