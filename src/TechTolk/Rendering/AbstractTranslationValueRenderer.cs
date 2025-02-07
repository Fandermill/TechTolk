using TechTolk.Division;
using TechTolk.TranslationSets.Values;

namespace TechTolk.Rendering;

/// <summary>
/// When implemented, it renders the resulting translation value into a <see cref="string"/>
/// </summary>
public abstract class AbstractTranslationValueRenderer
{
    /// <summary>
    /// Renders the translation value into a <see cref="string"/>
    /// </summary>
    /// <param name="divider">The divider the <see cref="TranslationValue"/> belongs to</param>
    /// <param name="value">The resulting value to be rendered</param>
    public virtual string Render(IDivider divider, TranslationValue value)
        => Render(divider, value, null);

    /// <summary>
    /// Renders the translation value into a <see cref="string"/>
    /// </summary>
    /// <param name="divider">The divider the <see cref="TranslationValue"/> belongs to</param>
    /// <param name="value">The resulting value to be rendered</param>
    /// <param name="data">Optional data to be merged into the result</param>
    public abstract string Render(IDivider divider, TranslationValue value, object? data);
}