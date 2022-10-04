namespace TechTolk.Compiling.Compiler;

public interface IMergerTolkCompiler<T>
{
    ITranslationSetTolkCompiler<T> WithMerger(ITranslationSetMerger<T> merger);
}
