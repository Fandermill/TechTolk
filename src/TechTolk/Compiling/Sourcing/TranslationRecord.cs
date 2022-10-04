using System;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Sourcing;

public class TranslationRecord<T> : ITranslationRecord<T>
{
    public TranslationSetInfo? SourceSetInfo { get; private set; }

    public IDivider Divider { get; private set; }

    public string Key { get; private set; }

    public T? Translation { get; private set; }

    public TranslationRecord(IDivider divider, string key, T? translation)
    {
        Divider = divider ?? throw new ArgumentNullException(nameof(divider));

        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("No key given", nameof(key));
        Key = key;

        Translation = translation;
    }

    public TranslationRecord<T> SetSource(ITranslationRecordSet<T> sourceSet)
    {
        return SetSource(sourceSet.SetInfo);
    }

    public TranslationRecord<T> SetSource(TranslationSetInfo? sourceSetInfo)
    {
        SourceSetInfo = sourceSetInfo;
        return this;
    }
}