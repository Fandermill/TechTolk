using TechTolk.Division;

namespace TechTolk.TranslationSets.Internals;

internal interface IInternalTranslationSet : ITranslationSet
{
    IInternalTranslationDictionary? GetDividerDictionary(IDivider divider);
    void SetDividerDictionary(IDivider divider, IInternalTranslationDictionary dictionary);
}
