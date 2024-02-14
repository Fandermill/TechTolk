using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Internals;

internal interface ITranslationDictionary
{
    TranslationValue Get(string key);
    TranslationValue? TryGet(string key);
}