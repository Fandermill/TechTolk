using TechTolk.TranslationSets.Options;

namespace TechTolk.TranslationSets.Merging;

internal class TranslationSetMergeOptions
{
    public string MergedSetName { get; set; } = "MergedSet";
    public DuplicateBehavior DuplicateBehavior { get; set; } = DuplicateBehavior.Replace;
}
