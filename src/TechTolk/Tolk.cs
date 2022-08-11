using System;
using System.Collections.Generic;

namespace TechTolk;

public class Tolk : ITolk
{
    private readonly ICurrentDividerProvider _currentDividerProvider;

    private IDictionary<Type, ITranslationSet> _typeSets;

    public Tolk(ICurrentDividerProvider currentDividerProvider)
    {
        _currentDividerProvider = currentDividerProvider;

        var translationSet = new TranslationSet<string>("Testing set");
        var nlDictionary = new TranslationDictionary<string>();
        nlDictionary.Add("TranslationKey", new Translation<string>("Translated text"));
        translationSet.AddDivision("nl", nlDictionary);

        _typeSets = new Dictionary<Type, ITranslationSet>();
        _typeSets.Add(typeof(string), translationSet);
    }



    public string Translate(string key)
    {
        return Translate<string>(key);
    }

    public string Translate(IDivider divider, string key)
    {
        return Translate<string>(divider, key);
    }

    public string Translate(IDivider divider, string key, object? data)
    {
        return Translate<string>(divider, key, data);
    }



    public T Translate<T>(string key)
    {
        var divider = _currentDividerProvider.GetCurrent();
        return Translate<T>(divider, key);
    }

    public T Translate<T>(IDivider divider, string key)
    {
        return Translate<T>(divider, key, null);
    }

    public T Translate<T>(IDivider divider, string key, object? data)
    {
        var type = typeof(T);
        if (!_typeSets.ContainsKey(type))
            throw new ArgumentException($"No translation set found for type '{type.Name}'");

        return _typeSets[type].GetTranslation<T>(divider.GetKey(), key);//data
    }
}
