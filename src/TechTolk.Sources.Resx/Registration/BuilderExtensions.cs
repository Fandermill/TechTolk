using System.Reflection;
using TechTolk.Sources.Resx;

namespace TechTolk.Registration.Builders;

public static class BuilderExtensions
{
    public static ITechTolkBuilder AddTranslationSetFromResource<TResxResource>(
        this ITechTolkBuilder techTolkBuilder)
        => AddTranslationSetFromResource<TResxResource>(techTolkBuilder, null);

    public static ITechTolkBuilder AddTranslationSetFromResource<TResxResource>(
        this ITechTolkBuilder techTolkBuilder, Action<ITranslationSetOptionsBuilder>? options)
    {
        techTolkBuilder.AddTranslationSet<TResxResource>(set =>
        {
            BuilderExtensions.FromResource<TResxResource>(set);
            if (options is not null)
            {
                set.WithOptions(options);
            }
        });

        return techTolkBuilder;
    }

    public static void FromResource<TResxResource>(
        this IRootTranslationSetRegistrationBuilder rootSetBuilder)
    {
        rootSetBuilder.RootBuilder.TryAddResx();

        rootSetBuilder.FromSource<ResxTranslationSetSource>(
            () => new ResxTranslationSetSourceOptions(typeof(TResxResource)));
    }

    public static void FromResource(
        this IRootTranslationSetRegistrationBuilder rootSetBuilder, string baseName, Assembly assembly)
    {
        rootSetBuilder.RootBuilder.TryAddResx();

        rootSetBuilder.FromSource<ResxTranslationSetSource>(
            () => new ResxTranslationSetSourceOptions(baseName, assembly));
    }

    public static void FromResource<TResxResource>(
        this IMergedTranslationSetRegistrationBuilder mergedSetBuilder)
    {
        mergedSetBuilder.RootBuilder.TryAddResx();

        var type = typeof(TResxResource);
        mergedSetBuilder.FromSource<ResxTranslationSetSource>(
            type.Name,
            () => new ResxTranslationSetSourceOptions(type));
    }

    public static void FromResource(
        this IMergedTranslationSetRegistrationBuilder mergedSetBuilder, string baseName, Assembly assembly)
    {
        mergedSetBuilder.RootBuilder.TryAddResx();

        mergedSetBuilder.FromSource<ResxTranslationSetSource>(
            baseName,
            () => new ResxTranslationSetSourceOptions(baseName, assembly));
    }
}