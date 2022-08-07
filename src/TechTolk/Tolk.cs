namespace TechTolk;

public class Tolk : ITolk
{
    private readonly ICurrentDividerProvider _currentDividerProvider;

    public Tolk(ICurrentDividerProvider currentDividerProvider)
    {
        _currentDividerProvider = currentDividerProvider;
    }

    public string Translate(string key)
    {
        var divider = _currentDividerProvider.GetCurrent();
        return Translate(divider, key);
    }

    public string Translate(IDivider divider, string key)
    {
        return Translate(divider, key, null);
    }

    public string Translate(IDivider divider, string key, object? data)
    {
        return "Translated text";
    }
}
