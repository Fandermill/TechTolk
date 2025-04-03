using TechTolk.Registration.Builders;
using TechTolk.Sources.Json;

namespace TechTolk;

public static class BuilderExtensions
{
    public static ITechTolkBuilder AddTranslationSetFromJson(
        this ITechTolkBuilder techTolkBuilder, string path)
        => techTolkBuilder.AddTranslationSetFromJson(path, null);

    public static ITechTolkBuilder AddTranslationSetFromJson<T>(
        this ITechTolkBuilder techTolkBuilder, string path)
        => techTolkBuilder.AddTranslationSetFromJson<T>(path, null);

    public static ITechTolkBuilder AddTranslationSetFromJson(
        this ITechTolkBuilder techTolkBuilder,
        string path,
        Action<ITranslationSetOptionsBuilder>? options)
    {
        string setName = Path.GetFileNameWithoutExtension(path);
        techTolkBuilder.AddTranslationSet(setName, RegisterSet(path, options));
        return techTolkBuilder;
    }

    public static ITechTolkBuilder AddTranslationSetFromJson<T>(
        this ITechTolkBuilder techTolkBuilder,
        string path,
        Action<ITranslationSetOptionsBuilder>? options)
    {
        techTolkBuilder.AddTranslationSet<T>(RegisterSet(path, options));
        return techTolkBuilder;
    }

    private static Action<IRootTranslationSetRegistrationBuilder> RegisterSet(
        string path,
        Action<ITranslationSetOptionsBuilder>? options)
    {
        return set =>
        {
            set.FromJson(path);

            if (options is not null)
            {
                set.WithOptions(options);
            }
        };
    }

    public static void FromJson(
        this IRootTranslationSetRegistrationBuilder rootSetBuilder,
        string path)
    {
        rootSetBuilder.RootBuilder.TryAddJson();

        rootSetBuilder.FromSource<JsonTranslationSetSource>(
            () => new JsonTranslationSetSourceOptions(path));
    }
}