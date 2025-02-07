using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechTolk.Division;
using TechTolk.Division.Internals;

namespace TechTolk.Registration.Builders;

internal sealed class DividerConfigurationBuilder : IDividerConfigurationBuilder
{
    public ITechTolkBuilder RootBuilder { get; private init; }

    private readonly SupportedDividersProvider _supportedDividersProvider;

    public DividerConfigurationBuilder(ITechTolkBuilder rootBuilder, SupportedDividersProvider supportedDividersProvider)
    {
        RootBuilder = rootBuilder;
        _supportedDividersProvider = supportedDividersProvider;
    }

    public IDividerConfigurationBuilder SetCurrentDividerProvider<T>() where T : ICurrentDividerProvider
    {
        RootBuilder.Services.Replace(new ServiceDescriptor(typeof(ICurrentDividerProvider), typeof(T), ServiceLifetime.Singleton));
        return this;
    }

    public IDividerConfigurationBuilder SetCurrentDividerProvider<T>(Func<IServiceProvider, ICurrentDividerProvider> provider) where T : ICurrentDividerProvider
    {
        RootBuilder.Services.Replace(new ServiceDescriptor(typeof(ICurrentDividerProvider), provider, ServiceLifetime.Singleton));
        return this;
    }

    public IDividerConfigurationBuilder AddSupportedDivider(IDivider divider)
    {
        _supportedDividersProvider.AddSupportedDivider(divider);
        return this;
    }
}