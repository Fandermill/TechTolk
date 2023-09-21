using TechTolk.Exceptions;
using TechTolk.TranslationSets.Options;

namespace TechTolk.Registration;

internal class TranslationSetRegistrationCollection
{
    private readonly Dictionary<string, TranslationSetRegistration> _translationSets;

    public TranslationSetRegistrationCollection()
    {
        _translationSets = new();
    }

    public TranslationSetRegistration Get(string key)
    {
        if (!_translationSets.TryGetValue(key, out var registration))
            throw new TranslationSetNotFoundException(key);
        return registration;
    }

    public TranslationSetRegistration Create(string name, TranslationSetOptions options)
    {
        var registration = new TranslationSetRegistration(name, options);

        if (!_translationSets.TryAdd(name, registration))
            throw new RegistrationException($"Duplicate translation set name '{registration.Key}'");
        return registration;
    }
}
