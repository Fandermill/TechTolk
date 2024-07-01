
# Getting Started

It requires a few steps to get started with TechTolk. First you have to configure 
TechTolk with your DI container and specify which dividers you will support. 
You also have to configure translation sets from which the translation values will 
be returned. You can then use an ITolk<T>, where <T> corresponds to a translation set,
by injecting it into your constructors and calling .Translate("key") where you need it.


## Registration

To register TechTolk services at application start, use the `IServiceCollection`
extension method `.AddTechTolk()`. With the returned builder, you can configure
TechTolk and add your translation sets.

```csharp
services
    .AddTechTolk()
    .UseCultureInfoDividers("nl-NL", "en-US")

    // (optional) set default behavior
    .WithOptions(o => o.OnTranslationNotFound().ReturnEmtpyString())

    // Add translation set from an embedded resource
    // with the TechTolk.Sources.Resx package
    .AddTranslationSetFromResource<MyResource>()

    // Or add translations from a JSON file (TechTolk.Sources.Json package)
    // This time with the more configurable methods
    // and also override the behavior upon 'translation not found'
    .AddTranslationSet("NamedSet", set => {
        set.FromJson("./MyTranslations.json");
        set.WithOptions(o => o.OnTranslationNotFound().ThrowException());
    });
```

This example uses the built in `CultureInfoDivider`. You will find more information
about dividers at the [dividers](dividers.md) page.

There are shorthand methods and more advanced methods to add your translation sets.
You can also add a translation set that is compiled from multiple sources into one.
More documentation on registering translation sets are on the [translation sets](translation-sets.md)
page.

Adding translation sets can be done with multiple implementations of sources. 
Read more about the possibilities on the [translation set sources](sources/index.md)
page.

## Tolk usage

To actually use TechTolk to render translations from your translation sets, you
need to aquire an ITolk instance from your service provider. You can then use
the `.Translate(string)` method to get your translations.


```csharp
public class MyClass
{
    private readonly ITolk<MyResourceTag> _tolk;

    public MyClass(ITolk<MyResourceTag> tolk)
    {
        _tolk = tolk;
    }

    public void MyMethod()
    {
        // From set source:
        // nl-NL:
        //   MyProfile: "Mijn profiel"
        //   UserGreeting: "Hallo {Username}"
        // en-US:
        //   MyProfile: "My profile"
        //   UserGreeting: "Hello {Username}"

        CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");

        // Uses the current divider (UICulture of current thread by default)
        Console.WriteLine("1: " + _tolk.Translate("MyProfile"));

        // You can always pass in a divider
        Console.WriteLine("2: " + _tolk.Translate("en-US", "MyProfile"));

        // Pass in a value bag to fill in the gaps
        Console.WriteLine("3: " + _tolk.Translate("UserGreeting", new { Username = "Fandermill"});

        // Outputs:
        //  "1: Mijn profiel"
        //  "2: My profile"
        //  "3: Hallo Fandermill"
    }
}
```

You can set the behavior of rendering by using an `IValueRenderer`. The built in
renderer features placeholders, but there is also a renderer that uses [SmartFormat](https://github.com/axuno/SmartFormat)
to have more formatting available in your translations. See the [value renderers](value-renderers.md)
page for more information.
