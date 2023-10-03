namespace TechTolk.Division.String;

public readonly struct StringDivider : IDivider
{
    public string Key { get; private init; }
    public StringDivider(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        Key = key;
    }
}