using Microsoft.Extensions.DependencyInjection;
using TechTolk.Division;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Building.Internals;

internal sealed class TranslationSetBuilderFactory : ITranslationSetBuilderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TranslationSetBuilderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITranslationSetBuilder CreateBuilder(SetInfo setInfo)
    {
        return new TranslationSetBuilder(
            _serviceProvider.GetRequiredService<ISupportedDividersProvider>(),
            _serviceProvider.GetRequiredService<IInternalTranslationSetFactory>(),
            _serviceProvider.GetRequiredService<ITranslationValueFactory>(),
            setInfo);
    }
}