using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
    // To make modern syntactic sugar features work for older targets
    internal class IsExternalInit { }
}

namespace System.Runtime.CompilerServices
{
#if !NET6_0_OR_GREATER

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }

#endif
}

#if !NET6_0_OR_GREATER

public static class TypeExtensions
{
    public static bool IsAssignableTo(this Type type, Type targetType)
    {
        return targetType.IsAssignableFrom(type);
    }
}

public static class DictionaryExtensions
{
    public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if(!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
            return true;
        }
        return false;
    }
}

#endif

namespace TechTolk.Sources.Resx
{
    // Because ArgumentNullException.ThrowIfNull and others are only
    // present in the more modern runtimes, I'm using this simple guard class
#if !NET7_0_OR_GREATER
    internal static class ArgumentNullException
    {
        public static void ThrowIfNull(object? value, [CallerArgumentExpression(nameof(value))] string? expression = default)
        {
            if (value is null)
                throw new System.ArgumentNullException(expression ?? "<unknown>");
        }
    }
#endif
}