using Microsoft.Extensions.DependencyInjection;
using TechTolk.Sources;
using TechTolk.Sources.Resx;

namespace TechTolk;

public static class TechTolkSourcesResxServiceCollectionExtensions
{
    public static void AddTechTolkResxServices(this IServiceCollection services)
    {
        services.AddSingleton(
            typeof(ITranslationSetSourceFactory<ResxTranslationSetSource>),
            typeof(ResxTranslationSetSourceFactory));

        services.AddSingleton<ResourceManagerTranslationSetSource>();
        services.AddSingleton<ResourceStreamTranslationSetSource>();
    }
}
