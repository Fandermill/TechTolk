using System;
using System.Collections.Generic;
using System.Linq;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Sourcing;

public class TranslationRecordSet<T> : ITranslationRecordSet<T>
{
    private Dictionary<(IDivider, string), ITranslationRecord<T>> _records;

    public TranslationSetInfo SetInfo { get; private set; }

    public IReadOnlyCollection<ITranslationRecord<T>> Records
        => _records.Select(kvp => kvp.Value).ToList().AsReadOnly();

    public TranslationRecordSet() : this(Guid.NewGuid().ToString()) { }
    public TranslationRecordSet(string name)
    {
        _records = new Dictionary<(IDivider, string), ITranslationRecord<T>>();
        SetInfo = new TranslationSetInfo(name, typeof(T));
    }

    public void AddRecord(ITranslationRecord<T> record)
    {
        AddRecord(record, true);
    }

    public void AddRecord(ITranslationRecord<T> record, bool replaceExisting)
    {
        var key = (record.Divider, record.Key);

        if (!_records.ContainsKey(key))
            _records.Add(key, record);
        else if (replaceExisting)
            _records[key] = record;
    }

    public void AddRecords(IEnumerable<ITranslationRecord<T>> records)
    {
        foreach (var record in records) 
            AddRecord(record);
    }

    public void AddRecords(IEnumerable<ITranslationRecord<T>> records, bool replaceExisting)
    {
        foreach (var record in records) 
            AddRecord(record, replaceExisting);
    }
}
