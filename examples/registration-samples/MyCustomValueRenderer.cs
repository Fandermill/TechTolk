using TechTolk.Division;
using TechTolk.Rendering;
using TechTolk.TranslationSets.Values;

namespace registration_samples;

public sealed class MyCustomValueRenderer : AbstractTranslationValueRenderer
{
    public override string Render(IDivider divider, TranslationValue value, object? data)
    {
        return value.Value + " (by custom renderer)";
    }
}