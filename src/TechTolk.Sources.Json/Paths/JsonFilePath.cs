using TechTolk.Division;

namespace TechTolk.Sources.Json.Paths;

internal readonly struct JsonFilePath
{
    public string Directory { get; init; }
    public string Name { get; init; }
    public string FullPath =>
        Path.Combine(
            Directory,
            Name + (Divider is null ? "" : "." + Divider.Key) + ".json"
            );

    public IDivider? Divider { get; init; }

    public JsonFilePath(string directory, string name)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("Directory cannot be null or whitespace", nameof(directory));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace", nameof(name));

        Directory = directory;
        Name = name;
    }

    public JsonFilePath(string directory, string name, IDivider divider) : this(directory, name)
    {
        Divider = divider;
    }
}
