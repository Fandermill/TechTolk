using TechTolk;
using TechTolk.Division;

namespace TechTolkTests.Helpers;

internal class TranslationSetBuilder
{
    /*private string _name = "TestSet";
    private Dictionary<string, TranslationDictionary> _dictionaries = new();

    public TranslationSetBuilder() { }

    public TranslationSetBuilder WithName(string name) { _name = name; return this; }


    public TranslationSetDividerBuilder ForDivider(IDivider divider)
        => ForDivider(divider.Key);
    public TranslationSetDividerBuilder ForDivider(string dividerKey)
    {
        return new TranslationSetDividerBuilder(this, dividerKey);
    }

    public TranslationSetBuilder Add(string dividerKey, string translationKey, string value)
    {
        _ = _dictionaries.TryAdd(dividerKey, new TranslationDictionary());

        _dictionaries[dividerKey].Add(translationKey, new TranslationValue(value));

        return this;
    }

    public ITranslationSet Build()
    {
        var set = new TranslationSet(new SetInfo(_name), new TranslationSetOptions());
        foreach(var kvp in _dictionaries)
        {
            set.SetDivisionDictionary(kvp.Key, kvp.Value);
        }
        return set;
    }
}

internal class TranslationSetDividerBuilder
{
    private readonly TranslationSetBuilder _setBuilder;
    private readonly string _dividerKey;

    internal TranslationSetDividerBuilder(TranslationSetBuilder setBuilder, string dividerKey)
    {
        ArgumentNullException.ThrowIfNull(setBuilder);
        ArgumentNullException.ThrowIfNull(dividerKey);

        _setBuilder = setBuilder;
        _dividerKey = dividerKey;
    }

    public TranslationSetDividerBuilder Add(string translationKey, string value)
    {
        _setBuilder.Add(_dividerKey, translationKey, value);
        return this;
    }

    public TranslationSetDividerBuilder ThenForDivider(IDivider divider)
        => ThenForDivider(divider.Key);
    public TranslationSetDividerBuilder ThenForDivider(string dividerKey)
        => _setBuilder.ForDivider(dividerKey);
    public ITranslationSet Build() => _setBuilder.Build();
    */
}