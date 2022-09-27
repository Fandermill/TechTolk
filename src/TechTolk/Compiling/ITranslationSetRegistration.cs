namespace TechTolk.Compiling;

public interface ITranslationSetRegistration<T> : ITranslationSetTolkCompiler<T>, ICompilableTolkCompiler<T>
{
    bool DiscardDuplicates { get; }
    ITranslationSet<T> GetTranslationSet();
    ITranslationSetRegistration<T> DiscardDuplicatesOnMerge();
}