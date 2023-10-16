using BCL=System.Globalization;
using TechTolk.Division.CultureInfo;

namespace TechTolk;

public static class ITolkExtensions
{
    public static string Translate(this ITolk tolk, BCL.CultureInfo cultureInfo, string key)
    {
        return tolk.Translate(cultureInfo, key, null);
    }

    public static string Translate(this ITolk tolk, BCL.CultureInfo cultureInfo, string key, object? data)
    {
        return tolk.Translate(CultureInfoDivider.FromCulture(cultureInfo), key, data);
    }
}