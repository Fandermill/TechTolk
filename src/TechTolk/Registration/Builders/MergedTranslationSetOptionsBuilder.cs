using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal class MergedTranslationSetOptionsBuilder : TranslationSetOptionsBuilder, IMergedTranslationSetOptionsBuilder
{
    private readonly TranslationSetMergeOptions _mergeOptions;

    public MergedTranslationSetOptionsBuilder(
        TranslationSetOptions options,
        TranslationSetMergeOptions mergeOptions) : base(options)
    {
        _mergeOptions = mergeOptions;
    }

    public IDuplicateKeyBehaviorConfigurationBuilder OnDuplicateKey()
    {
        return new DuplicateKeyBehaviorConfigurationBuilder(this, _mergeOptions);
    }
}
