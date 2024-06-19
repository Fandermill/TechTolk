using TechTolk.Division;
using TechTolk.TranslationSets.Options;

namespace TechTolk.TranslationSets.Building.Internals;

internal sealed class TranslationSetDictionaryBuilder : ITranslationSetDictionaryBuilder
{
    private readonly TranslationSetBuilder _setBuilder;
    private readonly IDivider _divider;

    internal TranslationSetDictionaryBuilder(TranslationSetBuilder setBuilder, IDivider divider)
    {
        ArgumentNullException.ThrowIfNull(setBuilder);
        ArgumentNullException.ThrowIfNull(divider);

        _setBuilder = setBuilder;
        _divider = divider;
    }

    public ITranslationSetDictionaryBuilder Add(string translationKey, string value)
        => Add(translationKey, value, DuplicateBehavior.Throw);

    public ITranslationSetDictionaryBuilder Add(IEnumerable<(string, string)> keyValuePairs)
    {
        foreach (var kvp in keyValuePairs)
            Add(kvp.Item1, kvp.Item2, DuplicateBehavior.Throw);
        return this;
    }

    public ITranslationSetDictionaryBuilder Add(string translationKey, string value, DuplicateBehavior duplicateBehavior)
    {
        _setBuilder.Add(_divider, translationKey, value, duplicateBehavior);
        return this;
    }

    public ITranslationSetDictionaryBuilder ThenForDivider(IDivider divider)
        => _setBuilder.ForDivider(divider);

    public ITranslationSet Build() => _setBuilder.Build();


}
