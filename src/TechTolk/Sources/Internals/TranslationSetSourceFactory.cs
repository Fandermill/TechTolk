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
        var factory = _serviceProvider.GetService(closedTypeSourceFactoryType) as ITranslationSetSourceFactory;
        if (factory is null)
        {
            if (HasParameterlessConstructor(sourceRegistration.Type))
            {
                return Activator.CreateInstance(sourceRegistration.Type, false) as ITranslationSetSource
                    ?? throw new RegistrationException(
                        $"Unable to instantiate '{sourceRegistration.Type.Name}' through it's parameterless " +
                        $"constructor. Please register a '{nameof(ITranslationSetSourceFactory)}' for it.");
            }
            else
            {
                throw new RegistrationException(
                    $"There is no registered factory type for source type '{sourceRegistration.Type.Name}'");
            }
        }

        return factory.Create(sourceRegistration);
    }

    private static bool HasParameterlessConstructor(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) is not null;
    }
}
