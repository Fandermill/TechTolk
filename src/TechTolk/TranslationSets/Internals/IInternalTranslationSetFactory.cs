using TechTolk.Division;

namespace TechTolk.TranslationSets.Internals;

internal interface IInternalTranslationSetFactory
{
    IInternalTranslationSet CreateEmpty(SetInfo setInfo, IEnumerable<IDivider> dividers);
}
