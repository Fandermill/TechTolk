using TechTolk.Division;

namespace TechTolk.TestSuite.Helpers.Dividers;

public readonly struct StringDivider : IDivider
{
    public string Key { get; init; }
    public StringDivider(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        Key = key;
    }
}