
# Translation set sources

Translation sets are populated from a source. A source could be anything, like a
database, a resource file or a hardcoded class, as long as it implements the interface
`ITranslationSetSource`. The core TechTolk package does not contain any implementations,
but extension packages exist.

These source packages are part of the TechTolk repository:

* TechTolk.Sources.Resx - Reads translations from embedded resource files
* ... more to come...

## Resource files (Resx)

The package `TechTolk.Sources.Resx` adds extension methods to load translations
from embedded resource files. First, register the required services with your
service container by calling `.AddTechTolkResxServices()`. Then you can register
a source by a type or via a basename and an assembly.

> [!WARNING]
> We really should look into avoiding the .AddTechTolkResxServices call by just
> calling 'TryAdd...' every time '.FromResource' is called.

The resource files should be named in the format: `{name}.{divider-key}.resx`.

```csharp
services.AddTechTolkResxServices();
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
> You can use an extension method to keep the translation set registration short
>
> ```csharp
> services.AddTechTolkResxServices();
> services.AddTechTolk()
>     //.ConfigureDividers(...)
>     .AddTranslationSetFromResource<MyResource>();
> 
>     // (Can later be requested by resolving ITolk<MyResource> from the service provider)
> ```

> [!WARNING]
> Be aware that you cannot register a resource source by a type tag (`.FromResource<T>()`)
> when using dividers other than `CultureInfo`. It will throw an exception.
> 
> This has to do with how the embedded resource files are loaded internally.
> When using the `CultureInfo.Name` as divider keys, like 'nl-NL' or 'en-US', a 
> `System.Resources.ResourceManager` is used, otherwise `Assembly.GetManifestResourceStream()`
> in combination with a `System.Resources.ResourceReader` is used. 

## Implementing your own custom source

If the sources from the TechTolk repository do not suffice for your project, you
can easily implement your own sources. You do this by implementing the `ITranslationSetSource`
interface with this method:

```csharp
Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
```

The `ITranslationSetBuilder` contains several methods to add translations for dividers.

You can use direct `Add` calls:

```csharp
public class MyTranslationSource : ITranslationSource
{
    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        var en = new CultureInfo("en-US");
        builder.Add(en, "MyKey", "MyTranslation");
        builder.Add(en, "AnotherKey", "AnotherTranslation");
        
        var nl = new CultureInfo("nl-NL");
        builder.Add(nl, "MyKey", "MijnVertaling");
        builder.Add(nl, "AnotherKey", "NogEenVertaling");

        return Task.CompletedTask;
    }
}
```

... or use the fluent API:

```csharp
public class MyTranslationSource : ITranslationSource
{
    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider(new CultureInfo("en-US")).Add(new[] {
                ( "MyKey", "MyTranslation"),
                ( "AnotherKey", "AnotherTranslation")
            })
            .ThenForDivider(new CultureInfo("nl-NL")).Add(new[] {
                ( "MyKey", "MijnVertaling"),
                ( "AnotherKey", "NogEenVertaling")
            });

        return Task.CompletedTask;
    }
}
```

## Registering your custom source

The most simplest way to use your custom source is to instantiate it and pass it
into the `.FromSource(...)` call.

```csharp
.AddTranslationSet("SetName", set => {
    set.FromSource(new MyTranslationSource());
});
```

If your custom source needs services injected that can be resolved from the service
provider, you can call `.FromSource<MyTranslationSource>()`. The source is resolved
as soon as the translation set gets compiled.

```csharp
.AddTranslationSet("SetName", set => {
    set.FromSource<MyTranslationSource>();
});
```

If your constructor is more complex, you must use a factory for your source by 
implementing the `ITranslationSetSourceFactory<>` interface, with the type of your
source as type argument. You must register the factory with your service container
yourself!

```csharp
.AddTranslationSet("SetName", set => {
    set.FromSource<SourceWithComplexConstructor>();
})

// (...)

public class SourceWithComplexConstructor : ITranslationSetSource
{
    public SourceWithComplexConstructor(string connectionString) { /* ... */ }

    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        /* ... */
    }
}

public class SourceWithComplexConstructorFactory : ITranslationSetSourceFactory<SourceWithComplexConstructor>
{
    // Don't forget to register this factory in the service container!

    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        return new SourceWithComplexConstructor("MyConnectionString");
    }
}
```