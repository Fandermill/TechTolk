using TechTolk.Registration;

namespace TechTolk.TranslationSets.Options.Internals;

internal class TranslationSetOptionsProvider : ITranslationSetOptionsProvider
{
    private readonly TranslationSetRegistrationCollection _registrations;

    public TranslationSetOptionsProvider(TranslationSetRegistrationCollection registrations)
    {
        _registrations = registrations;
    }

    public TranslationSetOptions GetByTranslationSetKey(string translationSetKey)
    {
        return _registrations.Get(translationSetKey).Options;
    }
}
