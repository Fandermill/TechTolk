using TechTolk.Division;
using TechTolk.TranslationSets.Values;

namespace TechTolk.TranslationSets;

public interface ITranslationSet
{
    SetInfo SetInfo { get; }
    TranslationValue? GetTranslation(IDivider divider, string translationKey);
}
