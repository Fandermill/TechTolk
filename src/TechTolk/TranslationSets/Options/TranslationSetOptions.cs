using TechTolk.Rendering.Internals;

namespace TechTolk.TranslationSets.Options;

public class TranslationSetOptions
{
    public TranslationNotFoundBehavior TranslationNotFoundBehavior { get; internal set; } = TranslationNotFoundBehavior.EmptyString;
    public Type ValueRendererType { get; internal set; } = typeof(ValueBagTranslationValueRenderer);

    internal TranslationSetOptions Clone()
    {
        return new TranslationSetOptions()
        {
            TranslationNotFoundBehavior = TranslationNotFoundBehavior,
            ValueRendererType = ValueRendererType
        };
    }
}
