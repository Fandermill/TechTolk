using TechTolk.Translations;

namespace TechTolk;

public interface ITranslationDictionary<T>
{
    ITranslation<T> Get(string key);
}
