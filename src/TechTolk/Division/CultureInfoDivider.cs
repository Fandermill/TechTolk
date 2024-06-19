using System.Globalization;

namespace TechTolk.Division;

/// <summary>
/// <see cref="IDivider"/> that is backed by a <see cref="CultureInfo"/>
/// </summary>
public readonly struct CultureInfoDivider : IDivider
{
    private readonly CultureInfo _cultureInfo;

    /// <summary>
    /// Creates a new instance, backed by the <see cref="CultureInfo.InvariantCulture"/>
    /// </summary>
    public CultureInfoDivider() { _cultureInfo = CultureInfo.InvariantCulture; }

    private CultureInfoDivider(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    /// <inheritdoc />
    public readonly string Key => _cultureInfo.Name;

    /// <summary>
    /// Creates a <see cref="CultureInfoDivider"/> from a culture name
    /// </summary>
    /// <param name="name">The name of the culture</param>
    public static CultureInfoDivider FromCulture(string name)
        => FromCulture(new CultureInfo(name));

    /// <summary>
    /// Creates a <see cref="CultureInfoDivider"/> from a <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="cultureInfo">The <see cref="CultureInfo"/> to act as the divider</param>
    public static CultureInfoDivider FromCulture(CultureInfo cultureInfo)
        => new CultureInfoDivider(cultureInfo);

    public static implicit operator CultureInfoDivider(string name) => FromCulture(name);
}
