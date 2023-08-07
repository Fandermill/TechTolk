using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Values;

namespace TechTolk.Registration.Builders;

internal class TranslationSetOptionsBuilder : ITranslationSetOptionsBuilder
{
    private readonly TranslationSetOptions _options;

    public TranslationSetOptionsBuilder(TranslationSetOptions options)
    {
        _options = options;
    }

    public ITranslationSetOptionsBuilder UseValueRenderer<T>() where T : ITranslationValueFactory
    {
        _options.ValueRendererType = typeof(T);
        return this;
    }

    public ITranslationNotFoundBehaviorConfigurationBuilder OnTranslationNotFound()
    {
        return new TranslationNotFoundBehaviorConfigurationBuilder(this, _options);
    }
}
