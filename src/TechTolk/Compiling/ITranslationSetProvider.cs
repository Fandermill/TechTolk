namespace TechTolk.Compiling;

public interface ITranslationSetProvider<T>
{
    ITranslationSet<T> GetTranslationSet();
}
