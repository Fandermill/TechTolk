using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using TechTolk.Division;
using TechTolk.Registration;

namespace TechTolk.Sources.Resx;

public class ResxTranslationSetSourceFactory : ITranslationSetSourceFactory<ResxTranslationSetSource>
{
    private static readonly Regex RFC_4647_REGEX = new Regex("^[A-Za-z]{1,8}(-[A-Za-z0-9]{1,8})*$");

    private readonly ISupportedDividersProvider _supportedDividersProvider;
    private readonly IServiceProvider _serviceProvider;

    public ResxTranslationSetSourceFactory(
        ISupportedDividersProvider supportedDividersProvider,
        IServiceProvider serviceProvider)
    {
        _supportedDividersProvider = supportedDividersProvider;
        _serviceProvider = serviceProvider;
    }

    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        bool dividerIsCulture = IsDividerACulture(_supportedDividersProvider.GetSupportedDividers().First());

        if(dividerIsCulture)
        {
            return _serviceProvider.GetRequiredService<ResourceManagerTranslationSetSource>();
        }
        else
        {
            return _serviceProvider.GetRequiredService<ResourceStreamTranslationSetSource>();
        }
    }

    private bool IsDividerACulture(IDivider divider)
    {
        return RFC_4647_REGEX.IsMatch(divider.Key);
    }
}



