using TechTolk.Sources.Json;

namespace TechTolk.Registration.Builders;

public static class BuilderExtensions
{
    public static ITechTolkBuilder AddTranslationSetFromJson(
        this ITechTolkBuilder techTolkBuilder, string path)
        => techTolkBuilder.AddTranslationSetFromJson(path, null);

    public static ITechTolkBuilder AddTranslationSetFromJson(
        this ITechTolkBuilder techTolkBuilder, string path, Action<ITranslationSetOptionsBuilder>? options)
    {
        string setName = Path.GetFileNameWithoutExtension(path);
        techTolkBuilder.AddTranslationSet(setName, set =>
        {
            set.FromJson(path);

            if (options is not null)
            {
                set.WithOptions(options);
            }
        });
        return techTolkBuilder;
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