namespace TechTolk;

public interface ITolkLoader
{
    void LoadTranslationSet(string setKey);
    void LoadTranslationSet<T>();
    void LoadTranslationSet(Type type);

    void ReloadTranslationSet(string setKey);
    void ReloadTranslationSet<T>();
    void ReloadTranslationSet(Type type);

    void ClearAllTranslationSets();
    void ClearTranslationSet(string setKey);
    void ClearTranslationSet<T>();
    void ClearTranslationSet(Type type);
}
