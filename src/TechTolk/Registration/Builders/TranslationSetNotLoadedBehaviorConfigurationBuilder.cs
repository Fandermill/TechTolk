using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal class TranslationSetNotLoadedBehaviorConfigurationBuilder : ITranslationSetNotLoadedBehaviorConfigurationBuilder
{
    private readonly TranslationSetOptionsBuilder _builder;
    private readonly TranslationSetOptions _options;

    public TranslationSetNotLoadedBehaviorConfigurationBuilder(
        TranslationSetOptionsBuilder builder,
        TranslationSetOptions options)
    {
        _builder = builder;
        _options = options;
    }

    public ITranslationSetOptionsBuilder ThrowException()
    {
        _options.TranslationSetNotLoadedBehavior = TranslationSetNotLoadedBehavior.Throw;
        return _builder;
    }

    public ITranslationSetOptionsBuilder LazyLoad()
    {
        _options.TranslationSetNotLoadedBehavior = TranslationSetNotLoadedBehavior.LazyLoad;
        return _builder;
    }
}
