namespace TechTolk.Compiling;

public interface ITranslationSetRegistration<T> : ITranslationSetTolkCompiler<T>, ICompilableTolkCompiler<T>
{
    ITranslationSetRegistration<T> DiscardDuplicates();
}