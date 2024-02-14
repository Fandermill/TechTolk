namespace TechTolk.TranslationSets.Building.Internals;

internal interface ITranslationSetBuilderFactory
{
    ITranslationSetBuilder CreateBuilder(SetInfo setInfo);
}
