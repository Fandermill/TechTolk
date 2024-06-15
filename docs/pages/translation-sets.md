
# Translation sets

A translation set is a collection of translations and consists of translation keys
and a translation per [divider](dividers). It gets registered with a name or a
type from which a name is determined. A set gets populated from a
[translation set source](translation-set-sources). Multiple sets can be merged
into one with a [merged translation set](#merged-translation-set).

## Registering a translation set

You can register a translation set with a name or with a type tag for easily injecting
an implementation of the `ITechTolk<T>` interface into your constructors. 
Then you need to register the source from which the translations should be loaded.
You can do this by invoking `.FromSource`.

> [!INFO]
> Find out more about the sources at the [Translation Set Sources](translation-set-sources) page.

Additionaly, you can set options for the behavior upon non existing translation values.

```csharp

services
    .AddTechTolk()
    //.ConfigureDividers(..)

    .AddTranslationSet("MyTranslationSet", set => {
        set.FromSource(new MyTranslationSetSource());
        set.WithOptions(o => o.OnTranslationNotFound().ReturnTranslationKey());
    })

    // The tag type corresponds when injecting ITechTolk<MyResource>
    .AddTranslationSet<MyResource>(set => {
        set.FromSource(new MyTranslationSetSource());
    });

```

### Options

You can set additional options by invoking `.WithOptions` on the set builder. With
these options, you can set the behavior upon requesting missing translations or
when the translation set is not yet loaded into memory.

The behavior upon requesting a translation that is not in the translation set can
be one of the following:
* Return translation key (returns the exact requested translation key as the translation)
* Return empty string
* Throw an exception

The behavior upon requesting a translation from the translation set that is not yet
loaded into memory:
* Lazy load (will try to load the whole translation set upon the first translation request)
* Throw an exception

```csharp
set.WithOptions(o => {
    o.OnTranslationNotFound().ReturnEmptyString()
     .OnTranslationSetNotLoaded().LazyLoad();
});
```

## Merged translation set

A merged translation set consists of translations from multiple sources. This could
be benificial for having a default set of translations that you can override with
your specifics. The sources don't have to match, so you could merge translations
from a resource file and translations from a database.

Registering a merged translation set works almost the same as a regular translation
set, except the `.FromSource` takes an additional `name` parameter and you can call
it multiple times. You also can configure the behavior on merging translations 
with the same translation key.

```csharp

services
    .AddTechTolk()
    //.ConfigureDividers(...)

    .AddMergedTranslationSet<MyResource>(set => {
        set.FromSource("Defaults", new MyTranslationSetSource());
        set.FromSource("Specifics", new AnotherTranslationSetSource());
    });

```

### Options

The options you can set are the same as normal translation sets, but there is an
additional option to configure the behavior on merging duplicate translation keys:
* Discard - keeps the translation from the first source
* Replace - replaces the translation of the previous sources with the last source
* Throw exception

```csharp
set.WithOptions(o => {
    o.OnDuplicateKey().Replace();
});
```