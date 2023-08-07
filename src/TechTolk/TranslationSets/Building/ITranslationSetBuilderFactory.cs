namespace TechTolk.TranslationSets.Building;

public interface ITranslationSetBuilderFactory
{
    ITranslationSetBuilder CreateBuilder(SetInfo setInfo);
}
