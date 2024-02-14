using TechTolk.Division;
using TechTolk.Exceptions;
using TechTolk.TranslationSets.Building;
using TechTolk.TranslationSets.Building.Internals;
using TechTolk.TranslationSets.Internals;

namespace TechTolk.TranslationSets.Merging;

internal class TranslationSetMerger : ITranslationSetMerger
{
    private readonly ITranslationSetBuilderFactory _builderFactory;
    private readonly ISupportedDividersProvider _supportedDividersProvider;

    public TranslationSetMerger(
        ITranslationSetBuilderFactory builderFactory,
        ISupportedDividersProvider supportedDividersProvider)
    {
        _builderFactory = builderFactory;
        _supportedDividersProvider = supportedDividersProvider;
    }

    public ITranslationSet Merge(TranslationSetMergeOptions options, params IInternalTranslationSet[] translationSets)
    {
        ThrowOnInvalidTranslationSetParameters(translationSets);

        var setInfo = new SetInfo(translationSets.First().SetInfo.Key, options.MergedSetName);
        var builder = _builderFactory.CreateBuilder(setInfo);

        foreach (var set in translationSets)
            AddSetToBuilder(set, builder, options);

        return builder.Build();
    }

    private void AddSetToBuilder(IInternalTranslationSet set, ITranslationSetBuilder builder, TranslationSetMergeOptions options)
    {
        foreach (var divider in _supportedDividersProvider.GetSupportedDividers())
        {
            var dictionary = set.GetDividerDictionary(divider);
            if (dictionary is not null)
                AddDictionaryToBuilder(divider, dictionary, builder, options);
        }
    }

    private void AddDictionaryToBuilder(
        IDivider divider,
        IInternalTranslationDictionary dictionary,
        ITranslationSetBuilder builder,
        TranslationSetMergeOptions options)
    {
        foreach (var translationKey in dictionary.Keys)
        {
            builder.Add(divider, translationKey, dictionary.Get(translationKey), options.DuplicateBehavior);
        }
    }



    private void ThrowOnInvalidTranslationSetParameters(IInternalTranslationSet[] translationSets)
    {
        if (translationSets.Length == 0)
            throw new MergeException("No translation sets given to merge");

        var keys = translationSets.Select(s => s.SetInfo.Key).Distinct();
        if (keys.Count() > 1)
            throw new MergeException($"Unable to merge translation keys with different set keys (found {string.Join(", ", keys)})");
    }
}
