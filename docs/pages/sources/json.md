# JSON translation set source

To get your translations from JSON files, you need to reference the 
`TechTolk.Sources.Json` package. It will read JSON files with the help of the
`System.Text.Json` libraries.

To load translations from JSON files, you simply call the `.FromJson()` method
with the path to the JSON files.

```csharp
services.AddTechTolk()
    //.ConfigureDividers(...)
    .AddTranslationSet<MyTag>(set => {
        set.FromJson("./PathToJson/MyTranslations.json");
    });
```

> [!TIP]
> You can use an extension method to keep the translation set registration
> short. The translation set name will be the given filename without the 
> extension.
>
> ```csharp
> services.AddTechTolk()
>     //.ConfigureDividers(...)
>     .AddTranslationSetFromJson("./PathToJson/MyTranslations.json");
>
>     // Translation set name will be 'MyTranslations'.
>     // Can later be requested by resolving an ITolkFactory from the 
>     // service provider and calling ITolkFactory.CreateTolk("MyTranslations")
> ```

## File naming

You can choose to put all the translations of all dividers into one file, or
you can split them up by divider. When splitting them up by divider, you should
use the format `{Filename}.{DividerKey}.json`.

TechTolk will automatically look for both by using the base path without
extension and trying every combination.

Thus, when configured `nl-NL` and `en-US` as supported dividers and calling
`.FromJson("./PathToJson/MyTranslations.json")` lets TechTolk check for:

* `./PathToJson/MyTranslations.json`
* `./PathToJson/MyTranslations.nl-NL.json`
* `./PathToJson/MyTranslations.en-US.json`

The path `./PathToJson/MyTranslations.fr-FR.json` will not be checked, because
`fr-FR` is not a supported divider.

## JSON format

TechTolk will search in the root object of the JSON for properties 
`translationSet` and `translationSets`. The plural forms, `translationSets` is 
nothing more than an array of the other.

The singular form `translationSet` should be an object with properties `divider`
and `translations`. The former should contain the divider key and the latter
should be an object of which the property/value pairs will be the translation
key and translation value pair.

When reading translation sets from files without the divider key in the
filename, the `divider` property is mandatory.

Maybe the format is best explained by example JSON:

```json
// Single set of translations for a specific divider
{
  "translationSet": {

    // This property is optional when the filename 
    // is found with the divider key suffix
    "divider": "en-US", 

    // The property/value pairs of this object 
    // represent the translation key/value pairs
    "translations": {
      "MyKey1": "MyValue",
      "MyKey2": "AnotherValue"
    }
  }
}

```

```json
// Translations for multiple dividers in one file
{
  "translationSets": [
    {
      "divider":"nl-NL",
      "translations": {
        "MyKey1": "MijnVertaling",
        "MyKey2": "MijnVertaling2"
      }
    },
    {
      "divider":"en-US",
      "translations": {
        "MyKey1": "MyTranslation1",
        "MyKey2": "MyTranslation2"
      }
    }
  ]
}
```
