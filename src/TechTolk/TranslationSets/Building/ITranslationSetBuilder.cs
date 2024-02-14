using TechTolk.Division;
using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Building;

/// <summary>
/// Builder for building an <see cref="ITranslationSet"/>
/// </summary>
public interface ITranslationSetBuilder
{
    /// <summary>
    /// Adds a translated string value to the translation set
    /// </summary>
    /// <param name="divider">The divider to which the translated value belongs to</param>
    /// <param name="translationKey">The translation key of which the value is a translation of</param>
    /// <param name="value">The translated value</param>
    /// <param name="duplicateBehavior">Behavior for when a value already exists for the translation key/divider combination</param>
    /// <returns>The same builder instance</returns>
    ITranslationSetBuilder Add(
        IDivider divider, 
        string translationKey, 
        string value, 
        DuplicateBehavior duplicateBehavior);

    /// <summary>
    /// Adds a translated string value to the translation set
    /// </summary>
    /// <param name="divider">The divider to which the translated value belongs to</param>
    /// <param name="translationKey">The translation key of which the value is a translation of</param>
    /// <param name="value">The translated value</param>
    /// <param name="duplicateBehavior">Behavior for when a value already exists for the translation key/divider combination</param>
    /// <returns>The same builder instance</returns>
    ITranslationSetBuilder Add(
        IDivider divider, 
        string translationKey, 
        TranslationValue value, 
        DuplicateBehavior duplicateBehavior);

    /// <summary>
    /// Select the given divider to chain multiple calls 
    /// without passsing in the <see cref="IDivider"/> every time
    /// </summary>
    /// <param name="divider">The divider to select</param>
    ITranslationSetDictionaryBuilder ForDivider(IDivider divider);

    /// <summary>
    /// Builds the resulting <see cref="ITranslationSet"/>
    /// </summary>
    /// <returns></returns>
    ITranslationSet Build();
}
