using System;

namespace TechTolk;

public class TranslationSetInfo
{
    public string Name { get; }
    public Type Type { get; }

    public TranslationSetInfo(string name, Type type)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }
}
