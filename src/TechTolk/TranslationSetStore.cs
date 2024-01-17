using System.Collections.Concurrent;
using TechTolk.Exceptions;
using TechTolk.Registration;
using TechTolk.Sources.Internals;
using TechTolk.TranslationSets;
using TechTolk.TranslationSets.Options;

namespace TechTolk;

internal class TranslationSetStore
{
    private readonly TranslationSetRegistrationCollection _registrations;
    private readonly ITranslationSetCompiler _translationSetCompiler;

    private readonly ConcurrentDictionary<string, ITranslationSet> _translationSets;

    public TranslationSetStore(
        TranslationSetRegistrationCollection registrations,
        ITranslationSetCompiler translationSetCompiler)
    {
        _registrations = registrations;
        _translationSetCompiler = translationSetCompiler;

        _translationSets = new();
    }

    public ITranslationSet GetTranslationSet(string setKey, TranslationSetNotLoadedBehavior notLoadedBehavior)
    {
        if (notLoadedBehavior == TranslationSetNotLoadedBehavior.LazyLoad)
            return GetOrAddTranslationSet(setKey);
        else
        {
            if (_translationSets.TryGetValue(setKey, out var set))
                return set;
            throw new TranslationSetNotLoadedException(setKey);
        }
    }

    internal ITranslationSet GetOrAddTranslationSet(string setKey)
    {
        return _translationSets.GetOrAdd(setKey, CompileSet);
    }

    private ITranslationSet CompileSet(string key)
    {
        var registration = _registrations.Get(key);
        return _translationSetCompiler.CompileTranslationSet(registration);
    }

    // TODO - Might want to add a Reset or Reload method here
}