namespace TechTolk;

public class Tolk<T> : ITolk<T>
{
    private readonly ICurrentDividerProvider _currentDividerProvider;

    private readonly ITranslationSet<T> _translations;

    public Tolk(ICurrentDividerProvider currentDividerProvider, ITranslationSet<T> translations)
    {
        _currentDividerProvider = currentDividerProvider;
        _translations = translations;
    }


    public T Translate(string key)
    {
        var divider = _currentDividerProvider.GetCurrent();
        return Translate(divider, key);
    }

    public T Translate(IDivider divider, string key)
    {
        return Translate(divider, key, null);
    }

    public T Translate(IDivider divider, string key, object? data)
    {
        return _translations.GetTranslation(divider.GetKey(), key, data);
    }
}