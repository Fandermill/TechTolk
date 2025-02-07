using Microsoft.Extensions.DependencyInjection;
using TechTolk.Registration.Builders;
using TechTolk.Rendering.Internals;
using TechTolk.Sources;
using TechTolk.Sources.Internals;
using TechTolk.TranslationSets.Building.Internals;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options.Internals;
using TechTolk.TranslationSets.Values;
using TechTolk.TranslationSets.Values.Internals;

namespace TechTolk;

/// <summary>
/// Extension methods for adding TechTolk to your DI container
/// </summary>
public static class TechTolkServiceCollectionExtensions
{
    /// <summary>
    /// Adds TechTolk to the service collection with default configuration
    /// </summary>
    public static ITechTolkBuilder AddTechTolk(this IServiceCollection services)
        => AddTechTolk(services, null);

    /// <summary>
    /// Adds TechTolk to the service collection
    /// </summary>
    /// <param name="defaults">Action to do configuration</param>
    public static ITechTolkBuilder AddTechTolk(
        this IServiceCollection services,
        Action<ITranslationSetOptionsBuilder>? defaults)
    {
        services.AddInternals();

        services.AddSingleton<ITolkLoader, TolkLoader>();
        services.AddSingleton<ITolkFactory, TolkFactory>();
        services.AddTransient(typeof(ITolk<>), typeof(Tolk<>));

        return new TechTolkBuilder(services, defaults);
    }

    private static void AddInternals(this IServiceCollection services)
    {
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