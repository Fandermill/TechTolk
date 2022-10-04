using System.Collections.Generic;

namespace TechTolk.Compiling.Sourcing;

public interface ITranslationRecordSet<T>
{
    TranslationSetInfo SetInfo { get; }
    IReadOnlyCollection<ITranslationRecord<T>> Records { get; }

    void AddRecord(ITranslationRecord<T> record);
    void AddRecords(IEnumerable<ITranslationRecord<T>> records);
}
