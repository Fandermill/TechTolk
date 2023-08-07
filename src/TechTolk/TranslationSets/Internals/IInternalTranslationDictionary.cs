using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Internals;

internal interface IInternalTranslationDictionary : ITranslationDictionary
{
    IEnumerable<string> Keys { get; }
    void Add(string key, TranslationValue value);
    void Replace(string key, TranslationValue value);
}
