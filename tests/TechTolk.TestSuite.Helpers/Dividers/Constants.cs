using TechTolk.Division;

namespace TechTolk.TestSuite.Helpers.Dividers;

public static class Constants
{
    public static class CultureInfoDividers
    {
        public static readonly CultureInfoDivider nl_NL = CultureInfoDivider.FromCulture("nl-NL");
        public static readonly CultureInfoDivider nl_BE = CultureInfoDivider.FromCulture("nl-BE");
        public static readonly CultureInfoDivider en_US = CultureInfoDivider.FromCulture("en-US");
    }

    public static class StringDividers
    {
        public static readonly StringDivider FixedDivider = new(nameof(FixedDivider));
        public static readonly StringDivider Div1 = new(nameof(Div1));
        public static readonly StringDivider Div2 = new(nameof(Div2));
    }
}