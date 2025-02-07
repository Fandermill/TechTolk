using TechTolk.Division;
using TechTolk.TranslationSets;

namespace TechTolk.Exceptions;

public sealed class TranslationKeyNotFoundException : NotFoundException
{
    private TranslationKeyNotFoundException(string message) : base(message) { }

    internal static TranslationKeyNotFoundException CreateFromTranslationSet(
        string translationKey, IDivider divider, ITranslationSet translationSet)
    {
        return new TranslationKeyNotFoundException(
            $"Key '{translationKey}' was not found for divider '{divider.Key}' " +
            $"in translation set '{translationSet.SetInfo.Key}'");
    }

    internal static TranslationKeyNotFoundException CreateFromTranslationDictionary(string translationKey)
    {
        return new TranslationKeyNotFoundException($"Key '{translationKey}' was not found in dictionary");
    }
}