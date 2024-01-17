using Microsoft.Extensions.DependencyInjection;
using TechTolk.Registration.Builders;
using TechTolk.Rendering.Internals;
using TechTolk.Sources;
using TechTolk.Sources.Internals;
using TechTolk.TranslationSets.Building;
using TechTolk.TranslationSets.Building.Internals;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options.Internals;
using TechTolk.TranslationSets.Values;
using TechTolk.TranslationSets.Values.Internals;

namespace TechTolk;

public static class TechTolkServiceCollectionExtensions
{
    public static ITechTolkBuilder AddTechTolk(this IServiceCollection services)
        => AddTechTolk(services, null);

    public static ITechTolkBuilder AddTechTolk(
        this IServiceCollection services,
        Action<ITranslationSetOptionsBuilder>? defaults)
    {
        services.AddInternals();

        services.AddSingleton<ITolkLoader, TolkLoader>();
        services.AddSingleton<ITolkFactory, TolkFactory>();
        services.AddSingleton(typeof(ITolk<>), typeof(Tolk<>));

        return new TechTolkBuilder(services, defaults);
    }

    private static void AddInternals(this IServiceCollection services)
    {
        // TODO - Lifetimes

        services.AddSingleton<ValueBagTranslationValueRenderer>();
        services.AddSingleton<ITranslationSetCompiler, TranslationSetCompiler>();
        services.AddSingleton<ITranslationSetSourceFactory, TranslationSetSourceFactory>();
        services.AddSingleton<ITranslationSetBuilderFactory, TranslationSetBuilderFactory>();
        services.AddSingleton<IInternalTranslationSetFactory, TranslationSetFactory>();
        services.AddSingleton<ITranslationSetMerger, TranslationSetMerger>();
        services.AddSingleton<ITranslationSetOptionsProvider, TranslationSetOptionsProvider>();
        services.AddSingleton<ITranslationValueFactory, TranslationValueFactory>();
        services.AddSingleton<ITolkFactory, TolkFactory>();
        services.AddSingleton<TranslationSetStore>();
    }
}