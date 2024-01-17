namespace TechTolk;

internal sealed class TolkLoader : ITolkLoader
{
    private readonly TranslationSetStore _translationSetStore;

    public TolkLoader(TranslationSetStore translationSetStore)
    {
        _translationSetStore = translationSetStore;
    }



    public void LoadTranslationSet(string setKey)
    {
        _ = _translationSetStore.GetOrAddTranslationSet(setKey);
    }

    public void LoadTranslationSet<T>() => LoadTranslationSet(typeof(T));

    public void LoadTranslationSet(Type type) => LoadTranslationSet(type.ToTranslationSetKey());



    public void ReloadTranslationSet(string setKey)
    {
        ClearTranslationSet(setKey);
        LoadTranslationSet(setKey);
    }

    public void ReloadTranslationSet<T>() => ReloadTranslationSet(typeof(T));

    public void ReloadTranslationSet(Type type) => ReloadTranslationSet(type.ToTranslationSetKey());



    public void ClearAllTranslationSets()
    {
        _translationSetStore.ClearAllTranslationSets();
    }

    public void ClearTranslationSet(string setKey)
    {
        _translationSetStore.ClearTranslationSet(setKey);
    }

    public void ClearTranslationSet<T>() => ClearTranslationSet(typeof(T));

    public void ClearTranslationSet(Type type) => ClearTranslationSet(type.ToTranslationSetKey());


}