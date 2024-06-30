using TechTolk.Sources.Json;

namespace TechTolk.Registration.Builders;

public static class BuilderExtensions
{
    public static void FromJson(
        this IRootTranslationSetRegistrationBuilder rootSetBuilder,
        string path)
    {
        rootSetBuilder.FromSource<JsonTranslationSetSource>(
            () => new JsonTranslationSetSourceOptions(path));
    }

}
