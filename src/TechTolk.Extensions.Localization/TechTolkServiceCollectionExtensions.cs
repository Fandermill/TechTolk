using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using TechTolk.Extensions.Localization.Adapters;

namespace TechTolk;

public static class TechTolkServiceCollectionExtensions
{
    public static IServiceCollection AddTechTolkLocalizationAdapters(this IServiceCollection services)
    {
        var factoryType = typeof(IStringLocalizerFactory);

        if (services.Any(s => s.ServiceType == factoryType))
        {
            services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, TechTolkStringLocalizerFactory>());
        }
        else
        {
            throw new InvalidOperationException(
                $"Unable to replace the {nameof(ServiceDescriptor)} of service type {factoryType.Name}, " +
                $"because no such descriptor is registered. \n" +
                $"Did you call {nameof(AddTechTolkLocalizationAdapters)} *after* calling {nameof(LocalizationServiceCollectionExtensions.AddLocalization)}?");
        }

        return services;
    }
}