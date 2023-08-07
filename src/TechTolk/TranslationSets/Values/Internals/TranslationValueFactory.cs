namespace TechTolk.TranslationSets.Values.Internals;

internal class TranslationValueFactory : ITranslationValueFactory
{
    public TranslationValue CreateValue(ITranslationSet translationSet, string value)
    {
        // Here might be room to detected special characters in the value
        // and determine if it contains HTML and such

        return new TranslationValue(translationSet.SetInfo, value);
    }
}
