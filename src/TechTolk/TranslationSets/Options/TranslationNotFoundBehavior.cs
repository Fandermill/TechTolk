namespace TechTolk.TranslationSets.Options;

/// <summary>
/// Behavior for when no translation value is present for the requested parameters
/// </summary>
public enum TranslationNotFoundBehavior
{
    /// <summary>
    /// An exception will be thrown
    /// </summary>
    Throw,

    /// <summary>
    /// An empty string will be returned
    /// </summary>
    EmptyString,

    /// <summary>
    /// The requested translation key will be returned
    /// </summary>
    TranslationKey
}
