using TechTolk.Dividing;

namespace TechTolk.Tests.Shared;

public class FixedStringDivider : IDivider
{
    private readonly string _key;

    public FixedStringDivider(string key)
    {
        _key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public string GetKey()
    {
        return _key;
    }
}
