using TechTolk.Division;
using TechTolk.TranslationSets;

namespace TechTolk.Exceptions;

public sealed class DividerNotFoundException : NotFoundException
{
    internal DividerNotFoundException(
        IDivider divider,
        ITranslationSet translationSet)
        : base(FormatMessage(divider, translationSet)) { }

    private static string FormatMessage(IDivider divider, ITranslationSet translationSet)
    {
        return $"No dictionary found for divider '{divider.Key}' in translation set '{translationSet.SetInfo.Key}'";
    }
}
