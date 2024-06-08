using System.Reflection;

namespace TechTolk.Sources.Resx;

internal sealed class ResxTranslationSetSourceOptions : TranslationSetSourceOptions
{
    /// <summary>
    /// todo
    /// </summary>
    public Type? ResourceType { get; init; }

    /// <summary>
    /// todo
    /// </summary>
    public string? BaseName { get; init; }

    /// <summary>
    /// The assembly from which the resource should be loaded
    /// </summary>
    public Assembly Assembly { get; init; }

    internal ResxTranslationSetSourceOptions(Type resourceType)
    {
        ArgumentNullException.ThrowIfNull(resourceType);

        ResourceType = resourceType;
        Assembly = resourceType.Assembly;
    }

    internal ResxTranslationSetSourceOptions(string baseName, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(baseName);
        ArgumentNullException.ThrowIfNull(assembly);

        BaseName = baseName;
        Assembly = assembly;
    }
}
