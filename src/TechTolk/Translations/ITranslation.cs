using System;

namespace TechTolk.Translations;

public interface ITranslation<T>
{
    Type Type { get; }
    T GetValue(object? data);
}
