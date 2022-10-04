using System;
using System.Collections.Generic;

namespace TechTolk.Compiling.Sourcing;

public class TranslationRecordSet<T> : ITranslationRecordSet<T>
{
    private List<ITranslationRecord<T>> _records;

    public TranslationSetInfo SetInfo { get; private set; }

    public IReadOnlyCollection<ITranslationRecord<T>> Records => _records.AsReadOnly();

    public TranslationRecordSet() : this(Guid.NewGuid().ToString()) { }
    public TranslationRecordSet(string name)
    {
        _records = new List<ITranslationRecord<T>>();
        SetInfo = new TranslationSetInfo(name, typeof(T));
    }

    public void AddRecord(ITranslationRecord<T> record)
    {
        _records.Add(record);
    }

    public void AddRecords(IEnumerable<ITranslationRecord<T>> records)
    {
        _records.AddRange(records);
    }
}
