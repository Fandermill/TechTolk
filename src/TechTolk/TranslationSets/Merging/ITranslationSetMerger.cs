using TechTolk.TranslationSets.Internals;

namespace TechTolk.TranslationSets.Merging;

internal interface ITranslationSetMerger
{
    ITranslationSet Merge(TranslationSetMergeOptions options, params IInternalTranslationSet[] translationSets);
}