using TechTolk.Compiling.Merging;

namespace TechTolk.Compiling.Compiler;

public interface IMergerTolkCompiler<T>
{
    ITranslationSetConverterTolkCompiler<T> WithMerger(ITranslationRecordSetMerger<T> merger);
}
