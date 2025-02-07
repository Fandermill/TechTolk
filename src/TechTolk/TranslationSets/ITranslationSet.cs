using TechTolk.Division;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets;

/// <summary>
/// A translation set represents a group of translation keys 
/// and it's translations for the configured dividers
/// </summary>
public interface ITranslationSet
{
    /// <summary>
    /// Metadata for this translation set
    /// </summary>
    SetInfo SetInfo { get; }

    /// <summary>
    /// Gets the translation value belonging to the translation key and divider
    /// </summary>
    /// <param name="divider">The divider to get the value for</param>
    /// <param name="translationKey">The translation key to get the value for</param>
    TranslationValue? GetTranslation(IDivider divider, string translationKey);
}