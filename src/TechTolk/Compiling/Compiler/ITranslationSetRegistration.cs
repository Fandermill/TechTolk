using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Compiler;

public interface ITranslationSetRegistration<T> : ITranslationSetTolkCompiler<T>, ICompilableTolkCompiler<T>
{
    bool DiscardDuplicates { get; }
    ITranslationRecordSet<T> GetTranslationSet();
    ITranslationSetRegistration<T> DiscardDuplicatesOnMerge();
}