using System.Collections.Generic;

namespace TechTolk.Compiling.Sourcing;

public interface ITranslationRecordSet<T>
{
    TranslationSetInfo SetInfo { get; }
    IReadOnlyCollection<ITranslationRecord<T>> Records { get; }

    void AddRecord(ITranslationRecord<T> record);
    void AddRecord(ITranslationRecord<T> record, bool replaceExisting);
    void AddRecords(IEnumerable<ITranslationRecord<T>> records);
    void AddRecords(IEnumerable<ITranslationRecord<T>> records, bool replaceExisting);
}
