using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal class TranslationNotFoundBehaviorConfigurationBuilder : ITranslationNotFoundBehaviorConfigurationBuilder
{
    private readonly TranslationSetOptionsBuilder _builder;
    private readonly TranslationSetOptions _options;

    public TranslationNotFoundBehaviorConfigurationBuilder(
        TranslationSetOptionsBuilder builder,
        TranslationSetOptions options)
    {
        _builder = builder;
        _options = options;
    }

    public ITranslationSetOptionsBuilder ReturnEmptyString()
    {
        _options.TranslationNotFoundBehavior = TranslationNotFoundBehavior.EmptyString;
        return _builder;
    }

    public ITranslationSetOptionsBuilder ReturnTranslationKey()
    {
        _options.TranslationNotFoundBehavior = TranslationNotFoundBehavior.TranslationKey;
        return _builder;
    }

    public ITranslationSetOptionsBuilder ThrowException()
    {
        _options.TranslationNotFoundBehavior = TranslationNotFoundBehavior.Throw;
        return _builder;
    }
}
