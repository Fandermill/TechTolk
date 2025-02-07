using System.Collections;
using System.Resources;
using TechTolk.Division;
using TechTolk.Exceptions;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources.Resx;

internal sealed class ResourceStreamTranslationSetSource : ResxTranslationSetSource
{
    private readonly ISupportedDividersProvider _supportedDividersProvider;

    public ResourceStreamTranslationSetSource(ISupportedDividersProvider supportedDividersProvider)
    {
        _supportedDividersProvider = supportedDividersProvider;
    }

    protected override Task PopulateTranslationsAsync(ITranslationSetBuilder builder, ResxTranslationSetSourceOptions options)
    {
        foreach (var divider in _supportedDividersProvider.GetSupportedDividers())
        {
            using var resourceStream = GetResourceStreamFromOptions(options, divider);
            using var reader = new ResourceReader(resourceStream);
            foreach (DictionaryEntry entry in reader)
            {
                if (entry.Key is string key && entry.Value is string val)
                {
                    builder.Add(divider, key, val);
                }
            }
        }

        return Task.CompletedTask;
    }

    private Stream GetResourceStreamFromOptions(ResxTranslationSetSourceOptions options, IDivider divider)
    {
        var resourceName = FormatResourceName(options, divider);

        var resourceStream = options.Assembly.GetManifestResourceStream(resourceName);

        if (resourceStream is null)
        {
            var names = options.Assembly.GetManifestResourceNames();

            string message = $"Unable to get resource stream by name '{resourceName}' from name '{options.BaseName}'.";
            if (names.Length == 0)
            {
                message += $" No manifest resource names found at all in assembly '{options.Assembly.FullName}'.";
            }
            else
            {
                message += "\n\n" +
                    $"Available resource names in assembly '{options.Assembly.FullName}':\n" +
                    string.Join("\n", names.Select(n => "- " + n).ToArray());
            }

            throw new InvalidOperationException(message);
        }

        return resourceStream;
    }

    private string FormatResourceName(ResxTranslationSetSourceOptions options, IDivider divider)
    {
        if (options.ResourceType is not null)
        {
            throw new RegistrationException(
                $"Options with '{nameof(options.ResourceType)}' set are not allowed " +
                $"in this '{nameof(ResourceStreamTranslationSetSource)}'. " +
                $"You should use .FromResource<TResxResource>() upon registering the " +
                $"translation set if you use dividers with the " +
                $"{nameof(System.Globalization.CultureInfo)} syntax.");
        }


        return $"{options.BaseName}.{divider.Key}.resources";
    }
}