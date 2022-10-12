using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Converting;

public interface ITranslationSetConverter<T>
{
    /* TODO - We can argue if this is the right name for this service */

    ITranslationSet<T> FromRecordSet(ITranslationRecordSet<T> recordSet);
}
