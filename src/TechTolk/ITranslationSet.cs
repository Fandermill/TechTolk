using System;

namespace TechTolk;

public interface ITranslationSet<T>
{
    string Name { get; }
    Type Type { get; }

    T GetTranslation(string divisionKey, string translationKey, object? data);
}
