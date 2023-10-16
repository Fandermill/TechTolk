using BCL=System.Globalization;

namespace TechTolk.Division.CultureInfo;

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