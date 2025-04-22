
# ![Tech Tolk Logo](https://raw.githubusercontent.com/Fandermill/TechTolk/main/assets/techtolk-logo-380x80.png)

TechTolk is a powerful and flexible .NET localization library with an extensible
set of sources and translation rendering features. It loads translation sets
into memory, registered through a simple-to-use API.
 
For full documentation, visit the 
[TechTolk documentation pages](https://fandermill.github.io/TechTolk).



## Features

* In-memory localizer
* Support for multiple sources, like JSON or Resx
* Merge multiple sources into one translation set
* Per translation set configuration
* Render translations with placeholders
* Extensible sources and value renderers
* Optional complex rendering with the use of 
  [`SmartFormat`](https://github.com/axuno/SmartFormat)
* Drop in adapter for use with 
  [Microsoft's localization](#net-localization-adapter)
* Targets .NET Standard 2.0 for use in both modern .NET and 
  classic .NET Framework applications



## Current versions

| Package                               | Version                                                                                                                                                                              |
| ------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `TechTolk`                            | ![TechTolk NuGet Version](https://img.shields.io/nuget/v/TechTolk?style=flat-square&logo=nuget&labelColor=2d93ad&color=DB5461)                                                       |
| `TechTolk.Sources.Json`               | ![TechTolk.Sources.Json NuGet Version](https://img.shields.io/nuget/v/TechTolk.Sources.Json?style=flat-square&logo=nuget&labelColor=2d93ad&color=DB5461)                             |
| `TechTolk.Sources.Resx`               | ![TechTolk.Sources.Resx NuGet Version](https://img.shields.io/nuget/v/TechTolk.Sources.Resx?style=flat-square&logo=nuget&labelColor=2d93ad&color=DB5461)                             |
| `TechTolk.ValueRenderers.SmartFormat` | ![TechTolk.ValueRenderers.SmartFormat NuGet Version](https://img.shields.io/nuget/v/TechTolk.ValueRenderers.SmartFormat?style=flat-square&logo=nuget&labelColor=2d93ad&color=DB5461) |
| `TechTolk.Extensions.Localization`    | ![TechTolk.Extensions.Localization NuGet Version](https://img.shields.io/nuget/v/TechTolk.Extensions.Localization?style=flat-square&logo=nuget&labelColor=2d93ad&color=DB5461)       |



## Getting Started

It requires a few steps to get started with TechTolk. First you have to
configure TechTolk with your DI container and specify which
[dividers](https://fandermill.github.io/TechTolk/pages/dividers.html) you will
support. You also have to configure translation sets from which the translation
values will be returned. You can then use an `ITolk<T>`, where `<T>` corresponds
to a translation set, by injecting it into your constructors and calling
`.Translate("key")` where you need it.

### Installation

Before you can use TechTolk, you need to add the core 
[NuGet package](https://www.nuget.org/packages/TechTolk) to your project.
TechTolk targets the `netstandard2.0` moniker, so you can use it for both modern
.NET and classic .NET Framework applications.

```
dotnet add package TechTolk
```

In the same way, add the 
[translation set source](https://fandermill.github.io/TechTolk/pages/sources/index.html)
package(s) of your liking as well, to be able to load your translations. For
example:

```
dotnet add package TechTolk.Sources.Resx
dotnet add package TechTolk.Sources.Json
```

### Registration

To register TechTolk services at application start, use the `IServiceCollection`
extension method `.AddTechTolk()`. With the returned builder, you can configure
TechTolk and add your translation sets.

```csharp
using TechTolk;

services
    .AddTechTolk()
    .UseCultureInfoDividers("nl-NL", "en-US")

    // (optional) set default behavior
    .ConfigureDefaultOptions(o => o.OnTranslationNotFound().ReturnEmptyString())

    // Add a translation set from an embedded resource
    // with the TechTolk.Sources.Resx package.
    // The MyResxTranslations type is the type of the 
    // embedded resource and can be used to request 
    // an ITolk instance from your DI container later on.
    .AddTranslationSetFromResource<MyResxTranslations>()

    // Or add a translation set from JSON files
    // with the TechTolk.Sources.Json package.
    // Use the MyResourceTag type to request an 
    // ITolk for this translation set later on.
    .AddTranslationSetFromJson<MyResourceTag>("./MyTranslations.json")

    // Or add a translation set with a custom name and 
    // override default behavior. Use an ITolkFactory 
    // to request an ITolk for a named set.
    .AddTranslationSet("NamedSet", set => {
        set.FromJson("./NamedSetTranslations.json");
        set.WithOptions(o => o.OnTranslationNotFound().ThrowException());
    });
```

See for adding a merged translation set that consists from multiple sources, the
[full documentation pages](https://fandermill.github.io/TechTolk/pages/translation-sets.html#merged-translation-set).

### Tolk usage

To actually use TechTolk to render translations from your translation sets, you
need to acquire an `ITolk` instance from your service provider. You can then use
the `.Translate(string)` method to get your translations.

```csharp
using TechTolk;

public class MyClass
{
    // MyResourceTag is the type of which you 
    // registered the translation set with earlier
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

        var nl_NL = new CultureInfo("nl-NL");
        var en_US = new CultureInfo("en-US");

        // The default implementation that provides the current divider
        // uses the current UI Culture of the current thread.
        // TechTolk is not responsible for setting the current divider.
        // For this example, set it to Dutch (nl-NL).
        CultureInfo.CurrentUICulture = nl_NL;

        // Translate using the current divider/culture
        Console.WriteLine("1: " + _tolk.Translate("MyProfile"));

        // Translate using a specific divider, like the en-US culture
        Console.WriteLine("2: " + _tolk.Translate(CultureInfoDivider.FromCulture(en_US), "MyProfile"));

        // Translate with placeholders filled using a value bag
        Console.WriteLine("3: " + _tolk.Translate("UserGreeting", new { Username = "Fandermill"}));

        // Outputs:
        //  "1: Mijn profiel"
        //  "2: My profile"
        //  "3: Hallo Fandermill"
    }
}
```

### .NET Localization adapter

Want to try TechTolk, but don't want to change all your translation calls in
your views? There is an additional library that you can use as a drop-in
replacement for .NET's localization implementation. See 
[.NET Localization Adapter](https://fandermill.github.io/TechTolk/pages/net-localization-adapter.html)
for more information.



## Support

If you encounter any issues or have questions, please open an issue on the 
[GitHub repository](https://github.com/Fandermill/TechTolk/issues).



## License

This project is licensed under the 
[MIT License](https://raw.githubusercontent.com/Fandermill/TechTolk/main/LICENSE).
Feel free to use, modify, and distribute it in your projects.