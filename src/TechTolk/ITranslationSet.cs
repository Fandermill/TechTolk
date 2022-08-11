using System;
using System.Collections.Generic;

namespace TechTolk;

public interface ITranslationSet<T>
{
    string Name { get; }
    Type Type { get; }

    T GetTranslation(string divisionKey, string translationKey, object? data);
}

public class TranslationSet<T> : ITranslationSet<T>
{
    public string Name { get; private set; }
    public Type Type => typeof(T);

    // todo private ITranslationDictionary<T> _dictionary;
    private Dictionary<string, TranslationDictionary<T>> _dividedDictionaries;

    public TranslationSet(string name)
    {
        Name = name;

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
}
