using System;
using System.Collections.Generic;

namespace TechTolk;

public interface ITranslationSet
{
    string Name { get; }
    Type Type { get; }

    TUnbox GetTranslation<TUnbox>(string divisionKey, string translationKey);
}

public class TranslationSet<T> : ITranslationSet
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

    public TUnbox GetTranslation<TUnbox>(string divisionKey, string translationKey)
    {
        var unboxType = typeof(TUnbox);
        if (!unboxType.IsAssignableFrom(Type))
            throw new InvalidCastException($"Unable to unbox translation of type '{Type.Name}' into '{unboxType.Name}'");

        // todo - better solution

        var translation= _dividedDictionaries[divisionKey].Get(translationKey);
        var result = translation.GetValue(null);
        return (TUnbox)(object)result;
    }
}
