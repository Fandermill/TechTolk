using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options;

namespace TechTolk.Registration.Builders;

internal sealed class DuplicateKeyBehaviorConfigurationBuilder : IDuplicateKeyBehaviorConfigurationBuilder
{
    private readonly IMergedTranslationSetOptionsBuilder _builder;
    private readonly TranslationSetMergeOptions _mergeOptions;

    public DuplicateKeyBehaviorConfigurationBuilder(
        IMergedTranslationSetOptionsBuilder builder,
        TranslationSetMergeOptions mergeOptions)
    {
        _builder = builder;
        _mergeOptions = mergeOptions;
    }

    public IMergedTranslationSetOptionsBuilder Discard()
    {
        _mergeOptions.DuplicateBehavior = DuplicateBehavior.Discard;
        return _builder;
    }

    public IMergedTranslationSetOptionsBuilder Replace()
    {
        _mergeOptions.DuplicateBehavior = DuplicateBehavior.Replace;
        return _builder;
    }

    public IMergedTranslationSetOptionsBuilder ThrowException()
    {
        _mergeOptions.DuplicateBehavior = DuplicateBehavior.Throw;
        return _builder;
    }
}
