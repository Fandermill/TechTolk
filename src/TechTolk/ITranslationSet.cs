using System.Collections.Generic;

namespace TechTolk;

public interface ITranslationSet<T>
{
    TranslationSetInfo SetInfo { get; }
    IReadOnlyDictionary<string, TranslationDictionary<T>> GetDivisionDictionaries();
    T GetTranslation(string divisionKey, string translationKey, object? data);
}
