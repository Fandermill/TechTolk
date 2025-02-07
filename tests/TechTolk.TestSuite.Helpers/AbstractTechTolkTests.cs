using Microsoft.Extensions.DependencyInjection;

namespace TechTolk.TestSuite.Helpers;

public abstract class AbstractTechTolkTests
{
    protected readonly IServiceCollection _services;

    private IServiceProvider? _provider;
    protected IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

    public AbstractTechTolkTests()
    {
        _services = new ServiceCollection();
    }

    protected ITolk GetTolkForTranslationSet(string setKey)
    {
        var factory = Provider.GetRequiredService<ITolkFactory>();
        return factory.Create(setKey);
    }

    protected ITolk<T> GetTolkForTranslationSet<T>()
    {
        return Provider.GetRequiredService<ITolk<T>>();
    }
}