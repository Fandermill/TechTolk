using System;

namespace TechTolk;

public interface ITranslation<T>
{
    Type Type { get; }
    T GetValue(object? data);
}

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