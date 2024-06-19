using Microsoft.Extensions.Localization;

namespace TechTolk.Extensions.Localization.Adapters;

internal sealed class TechTolkStringLocalizerFactory : IStringLocalizerFactory
{
    private ITolkFactory _tolkFactory;

    public TechTolkStringLocalizerFactory(ITolkFactory tolkFactory)
    {
        _tolkFactory = tolkFactory;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
        return new TechTolkStringLocalizer(_tolkFactory.Create(resourceSource));
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        string setName = (!string.IsNullOrWhiteSpace(location) ? location + "." : "") + baseName;
        return new TechTolkStringLocalizer(_tolkFactory.Create(setName));
    }
}