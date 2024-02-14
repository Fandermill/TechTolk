using TechTolk.Division;
using TechTolk.TranslationSets.Options;

namespace TechTolk.TranslationSets.Building;

public interface ITranslationSetDictionaryBuilder
{
	ITranslationSetDictionaryBuilder Add(string translationKey, string value);
	ITranslationSetDictionaryBuilder Add(IEnumerable<(string, string)> keyValuePairs);

    
    ITranslationSetDictionaryBuilder Add(string translationKey, string value, DuplicateBehavior duplicateBehavior);

	/// <summary>
	/// Switches to a different <see cref="IDivider"/>
	/// </summary>
	/// <param name="divider">The divider to select</param>
	ITranslationSetDictionaryBuilder ThenForDivider(IDivider divider);

	/// <inheritdoc cref="ITranslationSetBuilder.Build"/>
    ITranslationSet Build();
}
