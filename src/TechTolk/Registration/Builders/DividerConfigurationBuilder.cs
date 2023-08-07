using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechTolk.Division;
using TechTolk.Division.Internals;

namespace TechTolk.Registration.Builders;

internal class DividerConfigurationBuilder : IDividerConfigurationBuilder
{
    private readonly IServiceCollection _services;
    private readonly SupportedDividersProvider _supportedDividersProvider;

    public DividerConfigurationBuilder(IServiceCollection services, SupportedDividersProvider supportedDividersProvider)
    {
        _services = services;
        _supportedDividersProvider = supportedDividersProvider;
    }

    public IDividerConfigurationBuilder SetCurrentDividerProvider<T>() where T : ICurrentDividerProvider
    {
        _services.Replace(new ServiceDescriptor(typeof(ICurrentDividerProvider), typeof(T), ServiceLifetime.Singleton));
        return this;
    }

    public IDividerConfigurationBuilder SetCurrentDividerProvider<T>(Func<IServiceProvider, ICurrentDividerProvider> provider) where T : ICurrentDividerProvider
    {
        _services.Replace(new ServiceDescriptor(typeof(ICurrentDividerProvider), provider, ServiceLifetime.Singleton));
        return this;
    }

    public IDividerConfigurationBuilder AddSupportedDivider(IDivider divider)
    {
        _supportedDividersProvider.AddSupportedDivider(divider);
        return this;
    }
}
