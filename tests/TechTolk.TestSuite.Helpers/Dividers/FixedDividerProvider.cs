using TechTolk.Division;

namespace TechTolk.TestSuite.Helpers.Dividers;

public sealed class FixedDividerProvider : ICurrentDividerProvider
{
    private readonly StringDivider _divider;

    public FixedDividerProvider(string key)
    {
        _divider = new StringDivider(key);
    }

    public IDivider GetCurrent() => _divider;
}