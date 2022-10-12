using System.Collections.Generic;
using TechTolk.Compiling.Compiler;
using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Merging;

public class TranslationRecordSetMerger<T> : ITranslationRecordSetMerger<T>
{
    public ITranslationRecordSet<T> Merge(IEnumerable<ITranslationSetRegistration<T>> setRegistrations)
    {
        var resultSet = new TranslationRecordSet<T>("compiled-set");
        foreach (var setRegistration in setRegistrations)
        {
            MergeIntoResult(resultSet, setRegistration);
        }
        return resultSet;
    }

    private void MergeIntoResult(
        TranslationRecordSet<T> resultSet,
        ITranslationSetRegistration<T> setRegistration)
    {
        var set = setRegistration.GetTranslationSet();
        resultSet.AddRecords(set.Records, !setRegistration.DiscardDuplicates);
    }
}
