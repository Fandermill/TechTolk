using TechTolk.Division;
using TechTolk.Division.Internals;
using TechTolk.Sources;

namespace TechTolk.Registration.Builders;

public static class TechTolkBuilderExtensions
{
    /// <summary>
    /// Sets the supported dividers to <see cref="CultureInfoDivider"/> and uses
    /// the current thread ui culture as the <see cref="ICurrentDividerProvider"/>.
    /// This extension method serves as a shortcut for .ConfigureDividers(...).
    /// </summary>
    /// <param name="techTolkBuilder">The current builder</param>
    /// <param name="supportedDividers">
    /// The dividers to support. Strings are implicitly converted into <see cref="CultureInfoDivider"/>,
    /// but be sure to pass strings in the RFC_4647 format (like nl-NL or en-US).
    /// </param>
    /// <returns>The same current builder</returns>
    public static ITechTolkBuilder UseCultureInfoDividers(
        this ITechTolkBuilder techTolkBuilder, params CultureInfoDivider[] supportedDividers)
    {
        return techTolkBuilder.ConfigureDividers(builder =>
        {
            builder.SetCurrentDividerProvider<CurrentThreadUICultureDividerProvider>();
            foreach (var divider in supportedDividers)
            {
                builder.AddSupportedDivider(divider);
            }
        });
    }

    /// <summary>
    /// Shorthand extension method for adding a translation set with a source instance
    /// </summary>
    /// <param name="techTolkBuilder">The current builder</param>
    /// <param name="name">The name of the translation set</param>
    /// <param name="source">Instance of the translation set source</param>
    /// <returns>The current builder</returns>
    public static ITechTolkBuilder AddTranslationSetFromSource(
        this ITechTolkBuilder techTolkBuilder, string name, ITranslationSetSource source)
    {
        return techTolkBuilder.AddTranslationSet(name, set =>
        {
            set.FromSource(source);
        });
    }

    /// <summary>
    /// Shorthand extension method for adding a translation set with a source type.
    /// The type will also be the name of the translation set, so you can resolve
    /// an ITolk`T with the same type.
    /// </summary>
    /// <typeparam name="TTranslationSetSource">Type of the source of the translation set</typeparam>
    /// <param name="techTolkBuilder">The current builder</param>
    /// <returns>The current builder</returns>
    public static ITechTolkBuilder AddTranslationSetFromSource<TTranslationSetSource>(
        this ITechTolkBuilder techTolkBuilder)
        where TTranslationSetSource : ITranslationSetSource
    {
        return techTolkBuilder.AddTranslationSet<TTranslationSetSource>(set =>
        {
            set.FromSource<TTranslationSetSource>();
        });
    }

    /// <summary>
    /// Shorthand extension method for adding a translation set with a source type.
    /// </summary>
    /// <typeparam name="TTranslationSetSource">Type of the source of the translation set</typeparam>
    /// <param name="techTolkBuilder">The current builder</param>
    /// <param name="name">The name of the translation set</param>
    /// <returns>The current builder</returns>
    public static ITechTolkBuilder AddTranslationSetFromSource<TTranslationSetSource>(
        this ITechTolkBuilder techTolkBuilder, string name)
        where TTranslationSetSource : ITranslationSetSource
    {
        return techTolkBuilder.AddTranslationSet(name, set =>
        {
            set.FromSource<TTranslationSetSource>();
        });
    }
}
