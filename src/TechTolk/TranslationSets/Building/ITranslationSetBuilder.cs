using TechTolk.Division;
using TechTolk.TranslationSets.Options;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets.Building;

public interface ITranslationSetBuilder
{
    ITranslationSetBuilder Add(IDivider divider, string translationKey, string value, DuplicateBehavior duplicateBehavior);
    ITranslationSetBuilder Add(IDivider divider, string translationKey, TranslationValue value, DuplicateBehavior duplicateBehavior);
    ITranslationSetDictionaryBuilder ForDivider(IDivider divider);
    ITranslationSet Build();
}
