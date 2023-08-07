using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets;

public interface ITranslationDictionary
{
    TranslationValue Get(string key);
    TranslationValue? TryGet(string key);
}