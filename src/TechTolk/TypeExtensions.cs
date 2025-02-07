namespace TechTolk;

internal static class TypeExtensions
{
    public static string ToTranslationSetKey(this Type type)
    {
        return type.Name;
    }
}