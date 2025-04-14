
# <img src="./techtolk-logo.png" alt="TechTolk" height="80" />

TechTolk is a .NET localization library with an extensible set of sources and
translation rendering features. It loads translations sets into memory,
registered by a simple to use API.

See for full documentation: https://fandermill.github.io/TechTolk


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
[translation set source](https://fandermill.github.io/TechTolk/pages/sources/index.md)
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

    // Add translation set from an embedded resource
    // with the TechTolk.Sources.Resx package
    .AddTranslationSetFromResource<MyResxTranslations>()

    // Or add a translation set from JSON files
    // with the TechTolk.Sources.Json package
    .AddTranslationSetFromJson<MyResourceTag>("./MyTranslations.json")

    // Or add a translation set with a custom name and override default behavior
    .AddTranslationSet("NamedSet", set => {
        set.FromJson("./NamedSetTranslations.json");
        set.WithOptions(o => o.OnTranslationNotFound().ThrowException());
    });
```

### Tolk usage

To actually use TechTolk to render translations from your translation sets, you
need to aquire an ITolk instance from your service provider. You can then use
the `.Translate(string)` method to get your translations.

```csharp
using TechTolk;

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

        var nl_NL = new CultureInfo("nl-NL");
        var en_US = new CultureInfo("en-US");

        CultureInfo.CurrentUICulture = nl_NL;

        // Uses the current divider (UICulture of current thread by default)
        Console.WriteLine("1: " + _tolk.Translate("MyProfile"));

        // You can always pass in a divider
        Console.WriteLine("2: " + _tolk.Translate(CultureInfoDivider.FromCulture(en_US), "MyProfile"));

        // Pass in a value bag to fill in the gaps
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
[.NET Localization Adapter](https://fandermill.github.io/TechTolk/pages/net-localization-adapter.md)
for more information.