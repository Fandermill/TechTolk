namespace TechTolk.TranslationSets.Values;

/// <summary>
/// Represents a translated value from a <see cref="ITranslationSet"/>
/// </summary>
public readonly struct TranslationValue
{
    internal SetInfo Source { get; init; }

    // TODO - Might add some type, like indicating the value contains HTML or something.


    public string Value { get; init; }

    internal TranslationValue(SetInfo source, string value)
    {
        Source = source;

        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }
}
