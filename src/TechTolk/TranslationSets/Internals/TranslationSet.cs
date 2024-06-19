using TechTolk.Division;
using TechTolk.Exceptions;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Internals;

internal sealed class TranslationSet : ITranslationSet, IInternalTranslationSet
{
    public SetInfo SetInfo { get; private set; }




    private readonly Dictionary<string, IInternalTranslationDictionary> _divisionDictionaries;


    internal TranslationSet(SetInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);
        SetInfo = info;

        _divisionDictionaries = new();
    }

    public TranslationValue? GetTranslation(IDivider divider, string translationKey)
    {
        if (!_divisionDictionaries.ContainsKey(divider.Key))
            throw new DividerNotFoundException(divider, this);

        return _divisionDictionaries[divider.Key].TryGet(translationKey);
    }

    IInternalTranslationDictionary? IInternalTranslationSet.GetDividerDictionary(IDivider divider)
    {
        if (_divisionDictionaries.TryGetValue(divider.Key, out var dictionary))
            return dictionary;
        return null;
    }
    void IInternalTranslationSet.SetDividerDictionary(IDivider divider, IInternalTranslationDictionary dictionary)
    {
        if (_divisionDictionaries.ContainsKey(divider.Key))
            _divisionDictionaries[divider.Key] = dictionary;
        else
            _divisionDictionaries.Add(divider.Key, dictionary);
    }
}
