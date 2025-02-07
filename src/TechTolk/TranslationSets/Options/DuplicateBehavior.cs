namespace TechTolk.TranslationSets.Options;

/// <summary>
/// Behavior for encountering duplicate translation keys
/// while merging multiple translation sets together
/// </summary>
public enum DuplicateBehavior
{
    /// <summary>
    /// The latter input will replace the previous translation value
    /// </summary>
    Replace,

    /// <summary>
    /// The latter input will be discarded and the first one won't be touched
    /// </summary>
    Discard,

    /// <summary>
    /// An exception will be thrown
    /// </summary>
    Throw
}