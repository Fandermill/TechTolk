using Microsoft.Extensions.DependencyInjection.Extensions;
using TechTolk.Registration.Builders;
using TechTolk.Sources;
using TechTolk.Sources.Resx;

namespace TechTolk;

internal static class TechTolkSourcesResxServiceCollectionExtensions
{
    internal static ITechTolkBuilder TryAddResx(this ITechTolkBuilder builder)
    {
        builder.Services.TryAddSingleton(
            typeof(ITranslationSetSourceFactory<ResxTranslationSetSource>),
            typeof(ResxTranslationSetSourceFactory));

        builder.Services.TryAddSingleton<ResourceManagerTranslationSetSource>();
        builder.Services.TryAddSingleton<ResourceStreamTranslationSetSource>();

        return builder;
    }
}
