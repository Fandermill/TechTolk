namespace TechTolk.Compiling.Sourcing;

public interface ITranslationRecordSetProvider<T>
{
    ITranslationRecordSet<T> GetSet();
}
