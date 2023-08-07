using TechTolk.Exceptions;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Internals;

internal class TranslationDictionary : ITranslationDictionary, IInternalTranslationDictionary
{
    private readonly Dictionary<string, TranslationValue> _dictionary = new();
    IEnumerable<string> IInternalTranslationDictionary.Keys => _dictionary.Keys;



    public TranslationValue Get(string key)
        => TryGet(key) ?? throw TranslationKeyNotFoundException.CreateFromTranslationDictionary(key);

    public TranslationValue? TryGet(string key)
    {
        if (!_dictionary.TryGetValue(key, out var value))
            return null;
        return value;
    }



    void IInternalTranslationDictionary.Add(string key, TranslationValue value)
    {
        if (_dictionary.ContainsKey(key))
            throw DuplicateTranslationKeyException.CreateFromTranslationDictionary(key);
        _dictionary.Add(key, value);
    }

    void IInternalTranslationDictionary.Replace(string key, TranslationValue value)
    {
        _dictionary[key] = value;
    }
}
