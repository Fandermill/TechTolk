using System;
using System.Collections.Generic;
using TechTolk.Exceptions;
using TechTolk.Translations;

namespace TechTolk;

public class TranslationDictionary<T> : ITranslationDictionary<T>
{
    private readonly string _name;
    private readonly Dictionary<string, ITranslation<T>> _dictionary;

    public TranslationDictionary(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _dictionary = new Dictionary<string, ITranslation<T>>();
    }

    public void Add(string key, ITranslation<T> translation)
    {
        // for proof of concept
        _dictionary.Add(key, translation);
    }

    public ITranslation<T> Get(string key)
    {
        if (key is null) throw new ArgumentNullException(nameof(key));
        if (!_dictionary.ContainsKey(key)) 
            throw new TechTolkException($"Key '{key}' not found in dictionary '{_name}'");

        return _dictionary[key];
    }
}