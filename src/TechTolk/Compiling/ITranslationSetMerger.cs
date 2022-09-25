using System.Collections.Generic;

namespace TechTolk.Compiling;

public interface ITranslationSetMerger<T>
{
    ITranslationSet<T> Merge(IEnumerable<ITranslationSet<T>> sets);
}
