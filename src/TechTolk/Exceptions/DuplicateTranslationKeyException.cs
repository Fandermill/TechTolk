using TechTolk.Division;
using TechTolk.TranslationSets;

namespace TechTolk.Exceptions;

public sealed class DuplicateTranslationKeyException : TechTolkException
{
    private DuplicateTranslationKeyException(string message) : base(message) { }

    internal static DuplicateTranslationKeyException CreateFromTranslationSet(
        string translationKey, IDivider divider, ITranslationSet translationSet)
    {
        return new DuplicateTranslationKeyException(
            $"Key '{translationKey}' does already exist for divider '{divider.Key}' " +
            $"in translation set '{translationSet.SetInfo.Key}'");
    }

    internal static DuplicateTranslationKeyException CreateFromTranslationDictionary(string translationKey)
    {
        return new DuplicateTranslationKeyException($"Key '{translationKey}' is already present in the dictionary");
    }
}
