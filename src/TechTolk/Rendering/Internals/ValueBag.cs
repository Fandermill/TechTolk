namespace TechTolk.Rendering.Internals;

internal sealed class ValueBag
{
    private readonly Dictionary<string, object?> _data;

    public ValueBag()
    {
        _data = new Dictionary<string, object?>();
    }

    public ValueBag(Dictionary<string, object?> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        _data = data;
    }

    public object? this[string key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public void Set(string key, object? value)
    {
        if (_data.ContainsKey(key))
            _data[key] = value;
        else
            _data.Add(key, value);
    }

    public object? Get(string key)
    {
        if (_data.ContainsKey(key))
            return _data[key];
        return null;
    }
}