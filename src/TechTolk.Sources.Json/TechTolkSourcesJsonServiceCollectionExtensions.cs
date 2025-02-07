using Microsoft.Extensions.DependencyInjection.Extensions;
using TechTolk.Registration.Builders;
using TechTolk.Sources.Json;
using TechTolk.Sources.Json.Paths;

namespace TechTolk;

internal static class TechTolkSourcesJsonServiceCollectionExtensions
{
    internal static ITechTolkBuilder TryAddJson(this ITechTolkBuilder builder)
    {
        builder.Services.TryAddTransient<JsonTranslationSetSource>();
        builder.Services.TryAddSingleton<PathFinder>();
        builder.Services.TryAddSingleton<JsonDocumentTranslationSetReader>();
        return builder;
    }
}
