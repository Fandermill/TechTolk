using System.Collections.Generic;

namespace TechTolk;

public interface ITranslationDictionary<T>
{
    ITranslation<T> Get(string key);
}


public class TranslationDictionary<T> : ITranslationDictionary<T>
{
    private readonly Dictionary<string, ITranslation<T>> _dictionary;


    public TranslationDictionary()
    {
        _dictionary = new Dictionary<string, ITranslation<T>>();
    }

    public void Add(string key, ITranslation<T> translation)
    {
        // for proof of concept
        _dictionary.Add(key, translation);
    }

    public ITranslation<T> Get(string key)
    {
        // todo - guards
        return _dictionary[key];
    }
}