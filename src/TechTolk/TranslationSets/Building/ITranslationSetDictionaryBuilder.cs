using TechTolk.Division;
using TechTolk.TranslationSets.Options;

namespace TechTolk.TranslationSets.Building;

public interface ITranslationSetDictionaryBuilder
{
	ITranslationSetDictionaryBuilder Add(string translationKey, string value);
	ITranslationSetDictionaryBuilder Add(IEnumerable<(string, string)> keyValuePairs);

	ITranslationSetDictionaryBuilder Add(string translationKey, string value, DuplicateBehavior duplicateBehavior);
	ITranslationSetDictionaryBuilder ThenForDivider(IDivider divider);
    ITranslationSet Build();
}
