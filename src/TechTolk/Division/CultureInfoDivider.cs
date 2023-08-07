using System.Globalization;

namespace TechTolk.Division;

/// <summary>
/// <see cref="IDivider"/> that is backed by a <see cref="CultureInfo"/>
/// </summary>
public readonly struct CultureInfoDivider : IDivider
{
	private readonly CultureInfo _cultureInfo;

	public CultureInfoDivider(CultureInfo cultureInfo)
	{
		_cultureInfo = cultureInfo;
	}

	public readonly string Key => _cultureInfo.TwoLetterISOLanguageName;
}