using System.Collections;
using System.Globalization;
using System.Resources;
using TechTolk.Division;
using TechTolk.TranslationSets.Building;

namespace TechTolk.Sources.Resx;

internal sealed class ResourceManagerTranslationSetSource : ResxTranslationSetSource
{
    private readonly ISupportedDividersProvider _supportedDividersProvider;

    public ResourceManagerTranslationSetSource(ISupportedDividersProvider supportedDividersProvider)
    {
        _supportedDividersProvider = supportedDividersProvider;
    }
    
    protected override Task PopulateTranslationsAsync(ITranslationSetBuilder builder, ResxTranslationSetSourceOptions options)
    {
        var resourceManager = CreateResourceManagerFromOptions(options);
        Populate(builder, resourceManager, options);
        return Task.CompletedTask;
    }

    private void Populate(ITranslationSetBuilder builder, ResourceManager resourceManager, ResxTranslationSetSourceOptions options)
    {
        foreach (var divider in _supportedDividersProvider.GetSupportedDividers())
        {
            var cultureInfo = new CultureInfo(divider.Key);
            // TODO - validation?

            // TODO - try catch?

            using var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
            if (resourceSet is null)
            {
                throw new InvalidOperationException(
                    $"Unable to get resource set from resource manager " +
                    $"with base name '{resourceManager.ResourceSetType.Name}'");
            }
                

            foreach (DictionaryEntry? entry in resourceSet)
            {
                // TODO - Find out more about how this DictionaryEntry works, why the casting?
                if (entry?.Key is string key && entry?.Value is string val)
                {
                    // TODO - Duplicate behavior, from where?
                    builder.Add(divider, key, val, TranslationSets.Options.DuplicateBehavior.Replace);
                }
            }
        }
    }

    private ResourceManager CreateResourceManagerFromOptions(ResxTranslationSetSourceOptions options)
    {
        if (options.ResourceType is not null)
            return new ResourceManager(options.ResourceType);
        else if (options.BaseName is not null)
            return new ResourceManager(options.BaseName, options.Assembly);

        throw new ArgumentException("Unable to create a resource manager from the given options");
    }
}



