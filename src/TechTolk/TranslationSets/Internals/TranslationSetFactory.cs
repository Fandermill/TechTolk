using TechTolk.Division;

namespace TechTolk.TranslationSets.Internals;

internal sealed class TranslationSetFactory : IInternalTranslationSetFactory
{
    public IInternalTranslationSet CreateEmpty(SetInfo setInfo, IEnumerable<IDivider> dividers)
    {
        var set = new TranslationSet(setInfo);
        CreateDivisionDictionaries(set, dividers);
        return set;
    }

    private void CreateDivisionDictionaries(IInternalTranslationSet set, IEnumerable<IDivider> dividers)
    {
        foreach (var divider in dividers)
        {
            set.SetDividerDictionary(divider, new TranslationDictionary());
        }
    }
}
