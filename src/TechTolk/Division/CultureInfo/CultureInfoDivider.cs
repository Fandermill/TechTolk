using BCL = System.Globalization;

namespace TechTolk.Division.CultureInfo;

/// <summary>
/// <see cref="IDivider"/> that is backed by a <see cref="CultureInfo"/>
/// </summary>
public readonly struct CultureInfoDivider : IDivider
{
    private readonly BCL.CultureInfo _cultureInfo;

    public CultureInfoDivider() { _cultureInfo = BCL.CultureInfo.InvariantCulture; }
    private CultureInfoDivider(BCL.CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    public readonly string Key => _cultureInfo.Name;

    public static CultureInfoDivider FromCulture(string name)
        => FromCulture(new BCL.CultureInfo(name));

    public static CultureInfoDivider FromCulture(BCL.CultureInfo cultureInfo)
    {
        return new CultureInfoDivider(cultureInfo);
    }

    // TODO - is this useful?
    //public static explicit operator CultureInfoDivider(string cultureInfoName) => CultureInfoDivider.FromCulture(cultureInfoName);
}

public class Hoi
{

}