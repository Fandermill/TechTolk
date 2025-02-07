using System.Globalization;

namespace TechTolk.Division.Internals;

internal sealed class CurrentThreadUICultureDividerProvider : ICurrentDividerProvider
{
    public IDivider GetCurrent()
    {
        return CultureInfoDivider.FromCulture(CultureInfo.CurrentUICulture);
    }
}