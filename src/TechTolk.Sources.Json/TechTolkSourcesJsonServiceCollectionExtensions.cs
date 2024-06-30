using Microsoft.Extensions.DependencyInjection;
using TechTolk.Sources.Json;
using TechTolk.Sources.Json.Paths;

namespace TechTolk;

public static class TechTolkSourcesJsonServiceCollectionExtensions
{
    public static IServiceCollection AddTechTolkJsonServices(this IServiceCollection services)
    {
        services.AddTransient<JsonTranslationSetSource>();
        services.AddSingleton<PathFinder>();
        services.AddSingleton<JsonDocumentTranslationSetReader>();
        return services;
    }
}
