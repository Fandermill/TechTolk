using TechTolk.Division;
using TechTolk.Exceptions;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Building.Internals;

internal sealed class TranslationSetBuilder : ITranslationSetBuilder
{
    private readonly ISupportedDividersProvider _supportedDividersProvider;
    private readonly ITranslationValueFactory _valueFactory;

    private readonly IInternalTranslationSet _translationSet;
    private readonly Dictionary<string, IInternalTranslationDictionary> _dictionaries;

    public TranslationSetBuilder(
        ISupportedDividersProvider supportedDividersProvider,
        IInternalTranslationSetFactory translationSetFactory,
        ITranslationValueFactory valueFactory,
        SetInfo setInfo)
    {
        _supportedDividersProvider = supportedDividersProvider;
        _translationSet = translationSetFactory.CreateEmpty(setInfo, _supportedDividersProvider.GetSupportedDividers());
        _valueFactory = valueFactory;

        _dictionaries = new();
    }

    public ITranslationSetBuilder Add(IDivider divider, string translationKey, string value, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Throw)
    {
        var translationValue = _valueFactory.CreateValue(_translationSet, value);
        return Add(divider, translationKey, translationValue, duplicateBehavior);
    }

    public ITranslationSetBuilder Add(IDivider divider, string translationKey, TranslationValue value, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Throw)
    {
        ThrowOnUnsupportedDivider(divider);
        _ = _dictionaries.TryAdd(divider.Key, new TranslationDictionary());

        var existingValue = _dictionaries[divider.Key].TryGet(translationKey);
        if (existingValue is not null)
        {
            if (duplicateBehavior == DuplicateBehavior.Replace)
            {
                // + log
                _dictionaries[divider.Key].Replace(translationKey, value);
            }
            else if (duplicateBehavior == DuplicateBehavior.Throw)
            {
                // + log
                throw DuplicateTranslationKeyException.CreateFromTranslationSet(translationKey, divider, _translationSet);
            }
            else if (duplicateBehavior == DuplicateBehavior.Discard)
            {
                // + log
            }
        }
        else
        {
            _dictionaries[divider.Key].Add(translationKey, value);
        }

        return this;
    }

    public ITranslationSet Build()
    {
        var dividers = _supportedDividersProvider.GetSupportedDividers();

        foreach (var divider in dividers)
        {
            if (!_dictionaries.TryGetValue(divider.Key, out var dictionary))
                continue;

            _translationSet.SetDividerDictionary(divider, dictionary);
        }

        return _translationSet;
    }

    public ITranslationSetDictionaryBuilder ForDivider(IDivider divider)
    {
        ThrowOnUnsupportedDivider(divider);
        return new TranslationSetDictionaryBuilder(this, divider);
    }

    internal void ThrowOnUnsupportedDivider(IDivider divider)
    {
        if (!_supportedDividersProvider.IsSupportedDivider(divider))
            throw new UnsupportedDividerException(divider);
    }
}