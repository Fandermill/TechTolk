# JSON translation set source

To get your translations from JSON files, you need to reference the 
`TechTolk.Sources.Json` package. It will read JSON files with the help of the
`System.Text.Json` libraries.

> [!WARNING]
> We really should look into avoiding the .AddTechTolkJsonServices call by just
> calling 'TryAdd...' every time '.FromResource' is called.


To load translations from JSON files, you simply call the `.FromJson()` method
with the path to the JSON files.

```csharp
services.AddTechTolkJsonServices();
services.AddTechTolk()
    //.ConfigureDividers(...)
    .AddTranslationSet<MyTag>(set => {
        set.FromJson("./PathToJson/MyTranslations.json");
    });
```

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

TechTolk will in the root object of the JSON for properties `translationSet` and
`translationSets`. The plural forms, `translationSets` is nothing more than an
array of the other.

The singular form `translationSet` should be an object with properties `divider`
and `translations`. The former should contain the divider key and the latter is
an array of objects of which the property/value pairs will be the translation key
and translation value pair.

When reading translation sets from files without the divider key in the filename,
the `divider` property is mandatory.

Maybe the format is best explained by example JSON:

```json
// Single set of translations for a specific divider
{
  "translationSet": {

    // This property is optional when the filename 
    // is found with the divider key suffix
    "divider": "en-US", 

    "translations": [

      // You can use an object with multiple prop/value pairs
      {
        "MyKey1": "MyValue",
        "MyKey2": "AnotherValue"
      },

      // and/or with separate objects
      { "MyKey3":  "YetAnotherValue" }

    ]
  }
}

```

```json
// Translations for multiple dividers in one file
{
  "translationSets": [
    {
      "divider": "nl-NL",
      "translations": [
        // Using a single object (IN THE ARRAY) is fine
        { 
            "MyKey1": "MijnVertaling",
            "MyKey2": "MijnVertaling2"
        }
      ]
    },
    {
      "divider": "en-US",
      "translations": [

        // Using multiple objects is fine 
        { "MyKey1": "MyTranslation1" },
        { "MyKey2": "MyTranslation2" }

      ]
    }
  ]
}

```

> [!WARNING]
> TODO - We should look into allowing both OBJECT and ARRAY value kinds in 
> `translations` property.