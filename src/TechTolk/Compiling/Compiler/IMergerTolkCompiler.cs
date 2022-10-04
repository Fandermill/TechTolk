using TechTolk.Compiling.Merging;

namespace TechTolk.Compiling.Compiler;

public interface IMergerTolkCompiler<T>
{
    ITranslationSetTolkCompiler<T> WithMerger(ITranslationRecordSetMerger<T> merger);
}
