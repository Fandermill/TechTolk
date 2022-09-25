namespace TechTolk.Compiling;

public interface IMergerTolkCompiler<T>
{
    ITranslationSetTolkCompiler<T> WithMerger(ITranslationSetMerger<T> merger);
}
