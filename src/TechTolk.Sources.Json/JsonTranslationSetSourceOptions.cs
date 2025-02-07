namespace TechTolk.Sources.Json;

internal sealed class JsonTranslationSetSourceOptions : TranslationSetSourceOptions
{
    public string Path { get; init; }

    public JsonTranslationSetSourceOptions(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("No path given", nameof(path));
        }

        Path = path;
    }
}