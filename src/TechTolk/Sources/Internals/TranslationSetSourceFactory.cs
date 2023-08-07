using TechTolk.Exceptions;
using TechTolk.Registration;

namespace TechTolk.Sources.Internals;

internal class TranslationSetSourceFactory : ITranslationSetSourceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TranslationSetSourceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        var closedTypeSourceFactoryType = typeof(ITranslationSetSourceFactory<>).MakeGenericType(sourceRegistration.Type);
        var factoryType = _serviceProvider.GetService(closedTypeSourceFactoryType) as ITranslationSetSourceFactory
            ?? throw new RegistrationException($"There is no registered factory type for source type '{sourceRegistration.Type.Name}'");

        return factoryType.Create(sourceRegistration);
    }
}
