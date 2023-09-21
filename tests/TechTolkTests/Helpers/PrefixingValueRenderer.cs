using TechTolk.Division;
using TechTolk.Rendering;
using TechTolk.TranslationSets.Values;

namespace TechTolkTests.Helpers;

public class PrefixingValueRenderer : AbstractTranslationValueRenderer
{
    private readonly string _prefix;

    public PrefixingValueRenderer(string prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        _prefix = prefix;
    }

    public override string Render(IDivider divider, TranslationValue value, object? data)
    {
        return _prefix + ":" + value.Value;
    }
}
