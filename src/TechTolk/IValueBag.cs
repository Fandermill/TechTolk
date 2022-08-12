using System;
using System.Collections.Generic;

namespace TechTolk;

public interface IValueBag
{
    void Add(string key, object? value);
    object? Get(string key);

    object? this[string key] { get; set; }
}

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
        set
        {
            if (_data.ContainsKey(key))
                _data[key] = value;
            else
                Add(key, value);
        }
    }

    public void Add(string key, object? value)
    {
        if (!_data.ContainsKey(key))
            _data.Add(key, value);
        else
            throw new ArgumentException("Key does already exist");
    }

    public object? Get(string key)
    {
        if(_data.ContainsKey(key))
            return _data[key];
        return null;
    }
}