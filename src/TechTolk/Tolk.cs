using TechTolk.Division;
using TechTolk.Exceptions;
using TechTolk.Rendering;
using TechTolk.TranslationSets;
using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Options.Internals;
using TechTolk.TranslationSets.Values;

namespace TechTolk;

internal sealed class Tolk : ITolk
{
    private readonly ICurrentDividerProvider _currentDividerProvider;
    private readonly ITranslationSet _translationSet;
    private readonly TranslationSetOptions _options;
    private readonly AbstractTranslationValueRenderer _valueRenderer;

    public Tolk(
        ICurrentDividerProvider currentDividerProvider,
        ITranslationSet translationSet,
        TranslationSetOptions options,
        AbstractTranslationValueRenderer valueRenderer)
    {
        ArgumentNullException.ThrowIfNull(currentDividerProvider);
        ArgumentNullException.ThrowIfNull(translationSet);
        ArgumentNullException.ThrowIfNull(valueRenderer);

        _currentDividerProvider = currentDividerProvider;
        _translationSet = translationSet;
        _options = options ?? new TranslationSetOptions();
        _valueRenderer = valueRenderer;
    }

    public string Translate(string key)
    {
        var divider = _currentDividerProvider.GetCurrent();
        return Translate(divider, key);
    }

    public string Translate(string key, object? data)
    {
        var divider = _currentDividerProvider.GetCurrent();
        return Translate(divider, key, data);
    }

    public string Translate(IDivider divider, string key)
    {
        return Translate(divider, key, null);
    }

    public string Translate(IDivider divider, string key, object? data)
    {
        var value = _translationSet.GetTranslation(divider, key);
        if (value is null)
        {
            if (_options.TranslationNotFoundBehavior == TranslationNotFoundBehavior.Throw)
            {
                // + log
                throw TranslationKeyNotFoundException.CreateFromTranslationSet(key, divider, _translationSet);
            }
            else if (_options.TranslationNotFoundBehavior == TranslationNotFoundBehavior.TranslationKey)
            {
                // + log
                return key;
            }
            else // return empty string
            {
                // + log
                return string.Empty;
            }
        }

        return _valueRenderer.Render(divider, (TranslationValue)value, data);
    }
}

internal sealed class Tolk<T> : ITolk<T>
{
    private readonly ITolk _innerTolk;

    public Tolk(ITolkFactory factory)
    {
        _innerTolk = factory.Create<T>();
    }

    public string Translate(string key) => _innerTolk.Translate(key);
    public string Translate(string key, object? data) => _innerTolk.Translate(key, data);

    public string Translate(IDivider divider, string key) => _innerTolk.Translate(divider, key);

    public string Translate(IDivider divider, string key, object? data) => _innerTolk.Translate(divider, key, data);
}