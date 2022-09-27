using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TechTolk;

public class TranslationSet<T> : ITranslationSet<T>
{
    public TranslationSetInfo SetInfo { get; private set; }

    private Dictionary<string, TranslationDictionary<T>> _dividedDictionaries;

    public TranslationSet(string name)
    {
        SetInfo = new TranslationSetInfo(name, typeof(T));
        _dividedDictionaries = new Dictionary<string, TranslationDictionary<T>>();
    }

    public void AddDivision(string divisionKey, TranslationDictionary<T> dictionary)
    {
        // for testing out the idea
        _dividedDictionaries.Add(divisionKey, dictionary);
    }

    public T GetTranslation(string divisionKey, string translationKey, object? data)
    {
        if (!_dividedDictionaries.ContainsKey(divisionKey))
            throw new ArgumentException("Division key not found in translation set", nameof(divisionKey));
        
        var translation = _dividedDictionaries[divisionKey].Get(translationKey);
        return translation.GetValue(data);
    }

    public IReadOnlyDictionary<string, TranslationDictionary<T>> GetDivisionDictionaries()
    {
        return new ReadOnlyDictionary<string, TranslationDictionary<T>>(_dividedDictionaries);
    }
}
