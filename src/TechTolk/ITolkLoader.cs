namespace TechTolk;

public interface ITolkLoader
{
    Task LoadTranslationSetAsync(string setKey);
    Task LoadTranslationSetAsync<T>();
    Task LoadTranslationSetAsync(Type type);

    Task ReloadTranslationSetAsync(string setKey);
    Task ReloadTranslationSetAsync<T>();
    Task ReloadTranslationSetAsync(Type type);

    void ClearAllTranslationSets();
    void ClearTranslationSet(string setKey);
    void ClearTranslationSet<T>();
    void ClearTranslationSet(Type type);
}
