using System.Globalization;

namespace TechTolk.Division;

/// <summary>
/// <see cref="IDivider"/> that is backed by a <see cref="CultureInfo"/>
/// </summary>
public readonly struct CultureInfoDivider : IDivider
{
    private readonly CultureInfo _cultureInfo;

    public CultureInfoDivider() { _cultureInfo = CultureInfo.InvariantCulture; }
    private CultureInfoDivider(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    public readonly string Key => _cultureInfo.Name;

    public static CultureInfoDivider FromCulture(string name)
        => FromCulture(new CultureInfo(name));

    public static CultureInfoDivider FromCulture(CultureInfo cultureInfo)
    {
        return new CultureInfoDivider(cultureInfo);
    }
}
