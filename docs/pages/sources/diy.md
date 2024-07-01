# Implementing your own custom source

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