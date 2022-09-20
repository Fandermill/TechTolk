using System;

namespace TechTolk.Translations;

public class Translation<T> : ITranslation<T>
{
    public Type Type => typeof(T);

    private readonly T _value;

    public Translation(T value)
    {
        _value = value;
    }

    public T GetValue(object? data)
    {
        return _value;
    }
}