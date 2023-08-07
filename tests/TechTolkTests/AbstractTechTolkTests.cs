using Microsoft.Extensions.DependencyInjection;
using TechTolk;

namespace TechTolkTests;

public abstract class AbstractTechTolkTests
{
    protected readonly IServiceCollection _services;

    private IServiceProvider? _provider;
    protected IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

    public AbstractTechTolkTests()
    {
        _services = new ServiceCollection();
    }

    public ITolk GetTolkForTranslationSet(string setKey)
    {
        var factory = Provider.GetRequiredService<ITolkFactory>();
        return factory.Create(setKey);
    }
}