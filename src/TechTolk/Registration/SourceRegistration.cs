using TechTolk.Exceptions;
using TechTolk.Sources;

namespace TechTolk.Registration;

public abstract class SourceRegistrationBase
{
    public string Name { get; internal init; }
    internal TranslationSetSourceOptions Options { get; init; }

    protected SourceRegistrationBase(string name, TranslationSetSourceOptions? options)
    {
        Name = name ?? "AnonymousSource`" + Guid.NewGuid().ToString();
        Options = options ?? new TranslationSetSourceOptions();
    }

    public TOptions GetOptions<TOptions>() where TOptions : TranslationSetSourceOptions
        => Options as TOptions
        ?? throw new RegistrationException(
            $"Option object of source registration '{Name}' is not of " +
            $"requested options type '{typeof(TOptions).Name}'");
}

public class ResolveSourceRegistration : SourceRegistrationBase
{
    internal Type Type { get; init; }

    internal ResolveSourceRegistration(string name, Type type, TranslationSetSourceOptions? options)
        : base(name, options)
    {
        ArgumentNullException.ThrowIfNull(type);
        if (!type.IsAssignableTo(typeof(ITranslationSetSource)))
            throw new RegistrationException($"Given type '{type.Name}' must be implementing '{typeof(ITranslationSetSource).Name}'");
        Type = type;
    }
}

public class SourceInstanceRegistration : SourceRegistrationBase
{
    internal ITranslationSetSource Instance { get; init; }

    internal SourceInstanceRegistration(string name, ITranslationSetSource instance, TranslationSetSourceOptions? options)
        : base(name, options)
    {
        ArgumentNullException.ThrowIfNull(instance);
        Instance = instance;
    }
}