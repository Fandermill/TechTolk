namespace TechTolk.TranslationSets.Options.Internals;

internal interface ITranslationSetOptionsProvider
{
    TranslationSetOptions GetByTranslationSetKey(string translationSetKey);
}