using System;
using TechTolk.Values;

namespace TechTolk.Translations;

public class MergeTranslation<T> : ITranslation<T>
{
    private readonly T _value;
    private readonly IDataValueMerger<T> _dataValueMerger;

    public MergeTranslation(T value, IDataValueMerger<T> dataValueMerger)
    {
        _value = value;
        _dataValueMerger = dataValueMerger;
    }

    public Type Type => typeof(T);

    public T GetValue(object? data)
    {
        return _dataValueMerger.MergeDataIntoValue(_value, data);
    }
}
