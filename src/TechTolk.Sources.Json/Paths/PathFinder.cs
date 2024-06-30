using TechTolk.Division;

namespace TechTolk.Sources.Json.Paths;

internal sealed class PathFinder
{
    private readonly ISupportedDividersProvider _supportedDividersProvider;

    public PathFinder(ISupportedDividersProvider supportedDividersProvider)
    {
        _supportedDividersProvider = supportedDividersProvider;
    }

    public JsonFilePath[] FindExistingPaths(string path)
        => FindPaths(path).ToArray();

    private IEnumerable<JsonFilePath> FindPaths(string path)
    {
        string directory = Path.GetDirectoryName(path) ?? "/";
        string fileName = Path.GetFileNameWithoutExtension(path);

        var mainJsonFilePath = new JsonFilePath(directory, fileName);
        if (File.Exists(mainJsonFilePath.FullPath))
        {
            yield return mainJsonFilePath;
        }

        foreach (var divider in _supportedDividersProvider.GetSupportedDividers())
        {
            var jsonFilePath = new JsonFilePath(directory, fileName, divider);
            if (File.Exists(jsonFilePath.FullPath))
            {
                yield return jsonFilePath;
            }
        }
    }
}
