using System.Text.Json;
using TechTolk.Division;
using TechTolk.Sources.Json.Exceptions;
using TechTolk.Sources.Json.Paths;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources.Json;

internal sealed class JsonDocumentTranslationSetReader
{
    private const string SET_COLLECTION_PROPERTY = "translationSets";
    private const string SINGLE_SET_PROPERTY = "translationSet";
    private const string DIVIDER_PROPERTY = "divider";
    private const string TRANSLATIONS_PROPERTY = "translations";

    private readonly ISupportedDividersProvider _supportedDividersProvider;

    public JsonDocumentTranslationSetReader(ISupportedDividersProvider supportedDividersProvider)
    {
        _supportedDividersProvider = supportedDividersProvider;
    }

    public async Task ParseFileIntoSetBuilder(JsonFilePath jsonFile, ITranslationSetBuilder builder)
    {
        using var stream = File.OpenRead(jsonFile.FullPath);
        using var jsonDocument = await JsonDocument.ParseAsync(stream, new JsonDocumentOptions
        {
            CommentHandling = JsonCommentHandling.Skip
        });

        var root = new JsonElementWithPropertyName("$", jsonDocument.RootElement);

        bool anyFound = false;
        if (TryGetElementOfType(root, SET_COLLECTION_PROPERTY, JsonValueKind.Array, out var setCollection))
        {
            anyFound = true;
            foreach (var set in setCollection.EnumerateArray())
            {
                if (set.ValueKind != JsonValueKind.Object)
                {
                    throw new JsonFormatException(
                        $"'{SET_COLLECTION_PROPERTY}' should be an array of objects, " +
                        $"but found an array element of type {set.ValueKind}");
                }

                ParseSetElementIntoSetBuilder((SET_COLLECTION_PROPERTY, set), builder, jsonFile.Divider);
            }
        }

        if (TryGetElementOfType(root, SINGLE_SET_PROPERTY, JsonValueKind.Object, out var singleSet))
        {
            anyFound = true;
            ParseSetElementIntoSetBuilder((SINGLE_SET_PROPERTY, singleSet), builder, jsonFile.Divider);
        }


        if (!anyFound)
        {
            throw new JsonFormatException(
                $"No property found named '{SET_COLLECTION_PROPERTY}' " +
                $"or '{SINGLE_SET_PROPERTY}' " +
                $"in file {jsonFile.Name}");
        }
    }

    private void ParseSetElementIntoSetBuilder(
        JsonElementWithPropertyName set, ITranslationSetBuilder builder, IDivider? divider)
    {
        if (TryParseDividerFromSetElement(set, out var parsedDivider))
        {
            divider = parsedDivider;
        }

        if (divider is null)
        {
            throw new JsonFormatException(
                $"Expected translation set element to contain a '{DIVIDER_PROPERTY}' property, " +
                $"but no such property was found. " +
                $"The '{DIVIDER_PROPERTY}' is mandatory when the file was not loaded by divider key.");
        }

        var translationObjects = GetTranslationObjectsFromSetElement(set);
        if (!translationObjects.Any())
        {
            throw new JsonFormatException(
                "No translations were found in the translation set element. " +
                $"Make sure there is a property named '{TRANSLATIONS_PROPERTY}' " +
                "which should be an array of objects, or an object itself.");
        }

        foreach (var translationObject in translationObjects)
        {
            foreach (var objectProperty in translationObject.EnumerateObject())
            {
                var key = objectProperty.Name;
                var val = objectProperty.Value.GetString();

                if (val is not null)
                {
                    builder.Add(divider, key, val, TranslationSets.Options.DuplicateBehavior.Replace);
                }
            }
        }
    }

    private bool TryParseDividerFromSetElement(JsonElementWithPropertyName set, out IDivider parsedDivider)
    {
        if (TryGetElementOfType(set, DIVIDER_PROPERTY, JsonValueKind.String, out var dividerElement))
        {
            string? dividerKey = dividerElement.GetString();
            if (dividerKey is not null)
            {
                parsedDivider = _supportedDividersProvider.GetByKey(dividerKey);
                return true;
            }
        }

        parsedDivider = default!;
        return false;
    }

    private IEnumerable<JsonElement> GetTranslationObjectsFromSetElement(JsonElementWithPropertyName set)
    {
        if (TryGetElementOfType(set, TRANSLATIONS_PROPERTY, JsonValueKind.Object, out var translationObjectElement))
        {
            yield return translationObjectElement;
        }
        else if (TryGetElementOfType(set, TRANSLATIONS_PROPERTY, JsonValueKind.Array, out var translationsArrayElement))
        {
            foreach (var translationElement in translationsArrayElement.EnumerateArray())
            {
                if (translationElement.ValueKind != JsonValueKind.Object)
                {
                    throw new JsonFormatException(
                        $"'{TRANSLATIONS_PROPERTY}' should be an array of objects, " +
                        $"but found an array element of type {translationElement.ValueKind}");
                }

                yield return translationElement;
            }
        }
    }

    private static bool TryGetElementOfType(
        JsonElementWithPropertyName parent, string propertyName, JsonValueKind valueKind, out JsonElement result)
    {
        result = default!;

        if (parent.Element.TryGetProperty(propertyName, out var element))
        {
            if (element.ValueKind != valueKind)
            {
                return false;
            }

            result = element;
            return true;
        }
        return false;
    }

    private readonly struct JsonElementWithPropertyName
    {
        public string PropertyName { get; init; }
        public JsonElement Element { get; init; }

        public JsonElementWithPropertyName(string propertyName, JsonElement element)
        {
            PropertyName = propertyName;
            Element = element;
        }

        public static implicit operator JsonElementWithPropertyName((string p, JsonElement e) tuple)
            => new(tuple.p, tuple.e);
    }
}