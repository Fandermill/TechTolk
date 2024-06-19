using TechTolk.TranslationSets.Options;

namespace TechTolk.TranslationSets.Merging;

internal sealed class TranslationSetMergeOptions
{
    public string MergedSetName { get; set; } = "MergedSet";
    public DuplicateBehavior DuplicateBehavior { get; set; } = DuplicateBehavior.Replace;
}
