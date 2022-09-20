using TechTolk.Dividing;

namespace TechTolk.UnitTests;

public class FixedDividerProvider : ICurrentDividerProvider
{
    private readonly IDivider _divider;

    public FixedDividerProvider(IDivider fixedDivider)
    {
        _divider = fixedDivider;
    }

    public IDivider GetCurrent()
    {
        return _divider;
    }
}
