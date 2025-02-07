namespace TechTolk;

internal sealed class TolkLoader : ITolkLoader
{
    private readonly TranslationSetStore _translationSetStore;

    public TolkLoader(TranslationSetStore translationSetStore)
    {
        _translationSetStore = translationSetStore;
    }



    public async Task LoadTranslationSetAsync(string setKey)
    {
        _ = await _translationSetStore.InitTranslationSetAsync(setKey);
    }

    public Task LoadTranslationSetAsync<T>() => LoadTranslationSetAsync(typeof(T));

    public async Task LoadTranslationSetAsync(Type type) => await LoadTranslationSetAsync(type.ToTranslationSetKey());



    public async Task ReloadTranslationSetAsync(string setKey)
    {
        ClearTranslationSet(setKey);
        await LoadTranslationSetAsync(setKey);
    }

    public Task ReloadTranslationSetAsync<T>() => ReloadTranslationSetAsync(typeof(T));

    public async Task ReloadTranslationSetAsync(Type type) => await ReloadTranslationSetAsync(type.ToTranslationSetKey());



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