using TechTolk.Rendering.Internals;

namespace TechTolk.TranslationSets.Options;

public class TranslationSetOptions
{
    public TranslationSetNotLoadedBehavior TranslationSetNotLoadedBehavior { get; internal set; } = TranslationSetNotLoadedBehavior.LazyLoad;
    public TranslationNotFoundBehavior TranslationNotFoundBehavior { get; internal set; } = TranslationNotFoundBehavior.EmptyString;
    public Type ValueRendererType { get; internal set; } = typeof(ValueBagTranslationValueRenderer);

    internal TranslationSetOptions Clone()
    {
        return new TranslationSetOptions()
        {
            TranslationSetNotLoadedBehavior = TranslationSetNotLoadedBehavior,
            TranslationNotFoundBehavior = TranslationNotFoundBehavior,
            ValueRendererType = ValueRendererType
        };
    }
}
