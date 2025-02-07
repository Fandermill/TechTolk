using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using TechTolk.Extensions.Localization.Adapters;

namespace TechTolk;

public static class TechTolkServiceCollectionExtensions
{
    public static void AddTechTolkAspNetLocalization(this IServiceCollection services)
    {
        services.AddLocalization();
        services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, TechTolkStringLocalizerFactory>());
    }
}