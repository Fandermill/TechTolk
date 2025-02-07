namespace TechTolk;

/// <summary>
/// Factory for creating an <see cref="ITolk"/> for a translation set
/// </summary>
public interface ITolkFactory
{
    /// <summary>
    /// Creates an <see cref="ITolk"/> for the translation set with the given key
    /// </summary>
    /// <param name="translationSetKey">The key of the translation set</param>
    /// <returns>The <see cref="ITolk"/> for the requested translation set</returns>
    public ITolk Create(string translationSetKey);

    /// <summary>
    /// Creates an <see cref="ITolk"/> for the translation set of the given resource tag <typeparamref name="T" />
    /// </summary>
    /// <typeparam name="T">The resource tag type that identifies the translation set</typeparam>
    /// <returns>The <see cref="ITolk"/> for the requested translation set</returns>
    public ITolk Create<T>();

    /// <summary>
    /// Creates an <see cref="ITolk"/> for the translation set of the given resource tag type
    /// </summary>
    /// <param name="type">Type of the resource tag that identifies the translation set</param>
    /// <returns>The <see cref="ITolk"/> for the requested translation set</returns>
    public ITolk Create(Type type);
}