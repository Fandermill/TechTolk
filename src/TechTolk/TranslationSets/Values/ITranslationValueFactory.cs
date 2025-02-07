namespace TechTolk.TranslationSets.Values;

/// <summary>
/// Factory class for creating <see cref="TranslationValue"/> 
/// while loading <see cref="ITranslationSet"/>s into memory
/// </summary>
public interface ITranslationValueFactory
{
    /// <summary>
    /// Creates a <see cref="TranslationValue"/> from a string value
    /// </summary>
    /// <param name="translationSet">The translation set for which the translation value is created</param>
    /// <param name="value">The translated string value</param>
    TranslationValue CreateValue(ITranslationSet translationSet, string value);
}