using TechTolk.Division;
using TechTolk.TranslationSets.Values;

namespace TechTolk.Rendering;

public abstract class AbstractTranslationValueRenderer
{
    public virtual string Render(IDivider divider, TranslationValue value)
        => Render(divider, value, null);
    public abstract string Render(IDivider divider, TranslationValue value, object? data);
}
