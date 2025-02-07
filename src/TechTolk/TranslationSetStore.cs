using System.Collections.Concurrent;
using TechTolk.Exceptions;
using TechTolk.Registration;
using TechTolk.Sources.Internals;
using TechTolk.TranslationSets;
using TechTolk.TranslationSets.Options;

namespace TechTolk;

internal sealed class TranslationSetStore
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
        if (_translationSets.TryGetValue(setKey, out var set))
            return set;

        if (notLoadedBehavior == TranslationSetNotLoadedBehavior.LazyLoad)
            return InitTranslationSetSynchronously(setKey);

        throw new TranslationSetNotLoadedException(setKey);
    }

    internal async Task<ITranslationSet> InitTranslationSetAsync(string setKey)
    {
        if (_translationSets.TryGetValue(setKey, out var set))
            return set;

        var registration = _registrations.Get(setKey);
        var translationSet = await _translationSetCompiler.CompileTranslationSetAsync(registration);

        _translationSets.AddOrUpdate(setKey, translationSet, (key, oldValue) => translationSet);

        return translationSet;
    }

    private ITranslationSet InitTranslationSetSynchronously(string setKey)
    {
        var registration = _registrations.Get(setKey);
        var translationSet = _translationSetCompiler.CompileTranslationSetSynchronously(registration);

        _translationSets.AddOrUpdate(setKey, translationSet, (key, oldValue) => translationSet);

        return translationSet;
    }

    internal void ClearAllTranslationSets()
    {
        _translationSets.Clear();
    }

    internal void ClearTranslationSet(string setKey)
    {
        _translationSets.TryRemove(setKey, out _);
    }
}