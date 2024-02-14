using TechTolk.Division;

namespace TechTolk;

/// <summary>
/// The interface for getting translated values from a translation key
/// </summary>
public interface ITolk
{
    /// <summary>
    /// Gets the translated value from a translation key for the current divider
    /// </summary>
    /// <param name="key">The translation key to be translated</param>
    /// <returns>The translated value</returns>
    string Translate(string key);

    /// <summary>
    /// Gets the translated value from a translation key for the current divider
    /// and merges the given data into the translated value
    /// </summary>
    /// <param name="key">The translation key to be translated</param>
    /// <param name="data">The data to be merged into the translated value</param>
    /// <returns>The translated value with the data merged in</returns>
    string Translate(string key, object? data);

    /// <summary>
    /// Gets the translated value from a translation key for the given divider
    /// </summary>
    /// <param name="divider">The divider for which the translated value should be returned</param>
    /// <param name="key">The translation key to be translated</param>
    /// <returns>The translated value</returns>
    string Translate(IDivider divider, string key);

    /// <summary>
    /// Gets the translated value from a translation key for the given divider
    /// and merges the given data into the translated value
    /// </summary>
    /// <param name="divider">The divider for which the translated value should be returned</param>
    /// <param name="key">The translation key to be translated</param>
    /// <param name="data">The data to be merged into the translated value</param>
    /// <returns>The translated value with the data merged in</returns>
    string Translate(IDivider divider, string key, object? data);
}

/// <summary>
/// The interface for getting translated values from a translation key
/// </summary>
/// <typeparam name="T">The resource tag of a translation set</typeparam>
public interface ITolk<T> : ITolk { }