# The Tolk

The `ITolk` interface provides you with methods to render the translations from
a single translation set. 

## Aquiring an `ITolk`

As written on the [translation sets](translation-sets.md) page, you register a
translation set by a type tag or by a name. Registering by a type tag is handy,
because you can inject an `ITolk<T>` directly into your constructors. If you
registered your translation set by name, you have to use the `ITolkFactory`.

### By tag type

To aquire a tolk for a translation set you registered with a tag type, you
simply inject an `ITolk<T>` where `T` is the tag type you registered the
translation set with.

```csharp
// Registration

services
    .AddTechTolk()
    // (..)
    .AddTranslationSet<MyResource>(set => {
        /* ... */
    });
```

```csharp
// Injecting ITolk<T>

public class MyClass
{
    public MyClass(ITolk<MyResource> tolk) {
        // (..)
    }
}
```

### By name

When you registered your translation set by name, you can use the `ITolkFactory`
to get your `ITolk` instance for your translation set.

```csharp
// Registration

services
    .AddTechTolk()
    // (..)
    .AddTranslationSet("MyNamedTranslationSet", set => {
        /* ... */
    });
```csharp

```csharp
// Use ITolkFactory

public class MyClass
{
    private readonly ITolk _tolk;

    public MyClass(ITolkFactory tolkFactory)
    {
        _tolk = tolkFactory.CreateTolk("MyNamedTranslationSet");
    }
}
```


## Using an `ITolk`

Once you have an instance of an implementation of `ITolk`, you can start using
it. It contains a method `.Translate` to get your translations.

You can call `.Translate()` with only your translation key to render the
translation for the current divider. TechTolk will use the
`ICurrentDividerProvider` to determine this current divider.
(See [dividers page](dividers.md#the-icurrentdividerprovider).)

You can pass in a divider as well to explicitly get the translation for the
given divider.

To render translations with placeholder, you can pass in an anonymous object.
The built in `IValueRenderer` will merge the property values into the
translation by property names. More information on this is on the 
[value renderers](value-renderers.md) page.

```csharp

ITolk tolk = /* ... */;

// source of translation set
//
// nl-NL
// "MyProfile": "Mijn profiel"
// "UserGreeting": "Hallo {Username}"
//
// en-US
// "MyProfile": "My profiel"
// "UserGreeting": "Hello {Username}"

CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");

// Uses the current divider (UICulture of current thread by default)
Console.WriteLine("1: " + tolk.Translate("MyProfile"));

// You can always pass in a divider
Console.WriteLine("2: " + tolk.Translate("en-US", "MyProfile"));

// Pass in a value bag to fill in the gaps
Console.WriteLine("3: " + tolk.Translate("UserGreeting", new { Username = "Fandermill"});

// Outputs:
//  "1: Mijn profiel"
//  "2: My profile"
//  "3: Hallo Fandermill"

```

> [!WARNING]
> TODO - Check code!