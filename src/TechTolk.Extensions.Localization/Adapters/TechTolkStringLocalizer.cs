using Microsoft.Extensions.Localization;

namespace TechTolk.Extensions.Localization.Adapters;

internal class TechTolkStringLocalizer : IStringLocalizer
{
	private readonly ITolk _tolk;

	internal TechTolkStringLocalizer(ITolk tolk)
	{
		_tolk = tolk;
	}

	public LocalizedString this[string name] => GetLocalizedString(name);

	public LocalizedString this[string name, params object[] arguments] => GetLocalizedString(name, arguments);

	public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
	{
		throw new NotImplementedException("Sorry! Currently, this method is not implemented!");
	}

	private LocalizedString GetLocalizedString(string name)
		=> GetLocalizedString(name, null);

	private LocalizedString GetLocalizedString(string name, object[]? arguments)
	{
		var value = _tolk.Translate(name, arguments);
		return new LocalizedString(name, value);
	}
}
