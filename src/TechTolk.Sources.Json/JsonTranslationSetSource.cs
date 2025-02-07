using TechTolk.Registration;
using TechTolk.Sources.Json.Paths;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources.Json;

internal sealed class JsonTranslationSetSource : ITranslationSetSource
{
    private readonly PathFinder _pathFinder;
    private readonly JsonDocumentTranslationSetReader _documentParser;

    public JsonTranslationSetSource(PathFinder pathFinder, JsonDocumentTranslationSetReader documentParser)
    {
        _pathFinder = pathFinder;
        _documentParser = documentParser;
    }

    public async Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        var options = sourceRegistration.GetOptions<JsonTranslationSetSourceOptions>();

        var paths = _pathFinder.FindExistingPaths(options.Path);

        if (!paths.Any())
        {
            throw new ArgumentException(
                $"Could not find any json file paths for '{options.Path}'");
        }

        foreach (var path in paths)
        {
            await _documentParser.ParseFileIntoSetBuilder(path, builder);
        }
    }
}