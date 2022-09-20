using System.Collections.Generic;

namespace TechTolk.Values;

public class ValueBag : IValueBag
{
    private readonly Dictionary<string, object?> _data;

    public ValueBag()
    {
        _data = new Dictionary<string, object?>();
    }

    public object? this[string key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public void Set(string key, object? value)
    {
        if (_data.ContainsKey(key))
            _data[key] = value;
        else
            _data.Add(key, value);
    }

    public object? Get(string key)
    {
        if (_data.ContainsKey(key))
            return _data[key];
        return null;
    }
}