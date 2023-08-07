using Microsoft.Extensions.DependencyInjection;
using TechTolk.Division;
using TechTolk.Rendering;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk;

internal class TolkFactory : ITolkFactory
{
    private readonly TranslationSetStore _store;
    private readonly ITranslationSetOptionsProvider _optionsProvider;

    private readonly IServiceProvider _serviceProvider;

    public TolkFactory(
        TranslationSetStore store,
        ITranslationSetOptionsProvider optionsProvider,
        IServiceProvider serviceProvider)
    {
        _store = store;
        _optionsProvider = optionsProvider;
        _serviceProvider = serviceProvider;
    }

    private ICurrentDividerProvider GetCurrentDividerProvider()
        => _serviceProvider.GetRequiredService<ICurrentDividerProvider>();
    private AbstractTranslationValueRenderer GetValueRenderer(Type type)
        => (AbstractTranslationValueRenderer)_serviceProvider.GetRequiredService(type);


    public ITolk Create(string translationSetKey)
    {
        var set = _store.GetTranslationSet(translationSetKey);
        var options = _optionsProvider.GetByTranslationSetKey(set.SetInfo.Key);

        return new Tolk(
            GetCurrentDividerProvider(),
            set,
            options,
            GetValueRenderer(options.ValueRendererType));
    }

    public ITolk Create<T>() => Create(typeof(T));

    public ITolk Create(Type type)
    {
        var translationSetKey = GetTranslationSetKeyFromType(type);
        return Create(translationSetKey);
    }

    private string GetTranslationSetKeyFromType(Type type)
    {
        return type.Name;
    }
}