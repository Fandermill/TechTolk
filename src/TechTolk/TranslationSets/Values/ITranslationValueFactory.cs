namespace TechTolk.TranslationSets.Values;

public interface ITranslationValueFactory
{
    TranslationValue CreateValue(ITranslationSet translationSet, string value);
}
