# Embedded resource files (Resx)

The package `TechTolk.Sources.Resx` adds extension methods to load translations
from embedded resource files. You can register a source by a type or via a 
basename and an assembly.

The resource files should be named in the format: `{name}.{divider-key}.resx`.

```csharp
services.AddTechTolk()
    //.ConfigureDividers(...)
    .AddTranslationSet("MyResxSet", set => {
        
        // From resx files belonging to a type,
        // which are resource files with the same name as the type
        set.FromResource<MyResource>();

        // or

        // From a type name with the full namespace and the assembly containing 
        // the resource files
        set.FromResource("MyApplication.Resources.MyResource", typeof(MyApplication.SomeType).Assembly);
    });
```

> [!TIP]
> You can use an extension method to keep the translation set registration
> short.
>
> ```csharp
> services.AddTechTolk()
>     //.ConfigureDividers(...)
>     .AddTranslationSetFromResource<MyResource>();
> 
>     // (Can later be requested by resolving ITolk<MyResource> from the service provider)
> ```

> [!WARNING]
> Be aware that you cannot register a resource source by a type tag
> (`.FromResource<T>()`) when using dividers other than `CultureInfo`. It will
> throw an exception.
> 
> This has to do with how the embedded resource files are loaded internally.
> When using the `CultureInfo.Name` as divider keys, like 'nl-NL' or 'en-US', a
> `System.Resources.ResourceManager` is used, otherwise
> `Assembly.GetManifestResourceStream()` in combination with a
> `System.Resources.ResourceReader` is used. 