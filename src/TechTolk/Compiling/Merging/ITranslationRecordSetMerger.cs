using System.Collections.Generic;
using TechTolk.Compiling.Compiler;
using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Merging;

public interface ITranslationRecordSetMerger<T>
{
    ITranslationRecordSet<T> Merge(IEnumerable<ITranslationSetRegistration<T>> setRegistrations);
}
