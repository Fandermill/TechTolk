namespace TechTolk;

/// <summary>
/// Interface for loading, reloading and clearing translation sets
/// </summary>
public interface ITolkLoader
{
    /// <summary>
    /// Loads the translation set by it's key into memory
    /// </summary>
    /// <param name="setKey">The key of the translation set</param>
    Task LoadTranslationSetAsync(string setKey);

    /// <summary>
    /// Loads the translation set of the given resource tag <typeparamref name="T"/> into memory
    /// </summary>
    /// <typeparam name="T">The resource tag type that identifies the translation set</typeparam>
    Task LoadTranslationSetAsync<T>();

    /// <summary>
    /// Loads the translation set of the given resource tag type into memory
    /// </summary>
    /// <param name="type">Type of the resource tag that identifies the translation set</param>
    Task LoadTranslationSetAsync(Type type);




    /// <summary>
    /// Reloads the translation set by it's key
    /// </summary>
    /// <param name="setKey">The key of the translation set</param>
    Task ReloadTranslationSetAsync(string setKey);

    /// <summary>
    /// Reloads the translation set of the given resource tag <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The resource tag type that identifies the translation set</typeparam>
    Task ReloadTranslationSetAsync<T>();

    /// <summary>
    /// Reloads the translation set of the given resource tag type
    /// </summary>
    /// <param name="type">Type of the resource tag that identifies the translation set</param>
    Task ReloadTranslationSetAsync(Type type);





    /// <summary>
    /// Clears all loaded translation sets from memory
    /// </summary>
    void ClearAllTranslationSets();

    /// <summary>
    /// Clears the translation set from memory by it's key
    /// </summary>
    /// <param name="setKey">The key of the translation set</param>
    void ClearTranslationSet(string setKey);

    /// <summary>
    /// Clears the translation set of the given resource tag <typeparamref name="T"/> from memory
    /// </summary>
    /// <typeparam name="T">The resource tag type that identifies the translation set</typeparam>
    void ClearTranslationSet<T>();

    /// <summary>
    /// Clears the translation set of the given resource tag type from memory
    /// </summary>
    /// <param name="type">Type of the resource tag that identifies the translation set</param>
    void ClearTranslationSet(Type type);
}