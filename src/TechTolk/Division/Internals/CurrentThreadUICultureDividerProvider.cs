using BCL=System.Globalization;
using TechTolk.Division.CultureInfo;


namespace TechTolk.Division.Internals;

/// <summary>
/// This provider used <see cref="CultureInfo.CurrentUICulture" /> as the current <see cref="IDivider>"/>
/// </summary>
internal sealed class CurrentThreadUICultureDividerProvider : ICurrentDividerProvider
{
    public IDivider GetCurrent()
    {

        return CultureInfoDivider.FromCulture(BCL.CultureInfo.CurrentUICulture);
    }
}