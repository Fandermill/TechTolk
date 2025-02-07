using TechTolk.Exceptions;
using TechTolk.Registration;

namespace TechTolk.Sources.Internals;

internal sealed class TranslationSetSourceFactory : ITranslationSetSourceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Type _openGenericSourceFactoryType;

    public TranslationSetSourceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _openGenericSourceFactoryType = typeof(ITranslationSetSourceFactory<>);
    }

    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        var closedTypeSourceFactoryType = _openGenericSourceFactoryType.MakeGenericType(sourceRegistration.Type);

        if (_serviceProvider.GetService(closedTypeSourceFactoryType) is ITranslationSetSourceFactory factory)
        {
            return factory.Create(sourceRegistration);
        }

        if (_serviceProvider.GetService(sourceRegistration.Type) is ITranslationSetSource instance)
        {
            return instance;
        }

        if (HasParameterlessConstructor(sourceRegistration.Type))
        {
            return Activator.CreateInstance(sourceRegistration.Type, false) as ITranslationSetSource
                ?? throw new RegistrationException(
                    $"Unable to instantiate '{sourceRegistration.Type.Name}' through it's parameterless " +
                    $"constructor. Please register a '{nameof(ITranslationSetSourceFactory)}' for it " +
                    $"or register the type itself.");
        }
        else
        {
            throw new RegistrationException(
                $"No factory type nor the type itself was registered to " +
                $"instantiate a source of type '{sourceRegistration.Type.Name}'");
        }
    }

    private static bool HasParameterlessConstructor(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) is not null;
    }
}