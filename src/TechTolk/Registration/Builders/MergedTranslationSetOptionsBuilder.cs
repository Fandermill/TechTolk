using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal sealed class MergedTranslationSetOptionsBuilder : TranslationSetOptionsBuilder, IMergedTranslationSetOptionsBuilder
{
    private readonly TranslationSetMergeOptions _mergeOptions;

    public MergedTranslationSetOptionsBuilder(
        ITechTolkBuilder rootBuilder,
        TranslationSetOptions options,
        TranslationSetMergeOptions mergeOptions) : base(rootBuilder, options)
    {
        _mergeOptions = mergeOptions;
    }

    public IDuplicateKeyBehaviorConfigurationBuilder OnDuplicateKey()
    {
        return new DuplicateKeyBehaviorConfigurationBuilder(this, _mergeOptions);
    }
}