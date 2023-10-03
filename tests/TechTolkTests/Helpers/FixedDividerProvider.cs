using TechTolk.Division;
using TechTolk.Division.String;

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