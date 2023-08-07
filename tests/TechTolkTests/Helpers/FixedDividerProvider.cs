using TechTolk.Division;

namespace TechTolkTests.Helpers;

internal class FixedDividerProvider : ICurrentDividerProvider
{
    private readonly StringDivider _divider;

    public FixedDividerProvider(string key)
    {
        _divider = new StringDivider(key);
    }

    public IDivider GetCurrent() => _divider;


}

internal readonly struct StringDivider : IDivider
{
    public string Key { get; init; }
    public StringDivider(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        Key = key;
    }
}