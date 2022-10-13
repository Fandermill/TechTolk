using TechTolk.Translations;

namespace TechTolk.Compiling.Parsing;

/// <summary>
/// Implementations of this interface should create <see cref="ITranslation{T}"/>
/// objects based on the given value from a <see cref="ITranslationRecord{T}"/>
/// </summary>
/// <typeparam name="T">The type of the value of the translation</typeparam>
public interface ITranslationValueParser<T>
{
    ITranslation<T>? Parse(T? value);
}
