
# Getting Started

It requires a few steps to get started with TechTolk. First you have to
configure TechTolk with your DI container and specify which
[dividers](./dividers.md) you will support. You also have to configure
translation sets from which the translation values will be returned. You can
then use an `ITolk<T>`, where `<T>` corresponds to a translation set, by
injecting it into your constructors and calling `.Translate("key")` where you
need it.

## Installation

Before you can use TechTolk, you need to add the core 
[NuGet package](https://www.nuget.org/packages/TechTolk) to your project.
TechTolk targets the `netstandard2.0` moniker, so you can use it for both modern
.NET and classic .NET Framework applications.

# [dotnet CLI](#tab/dotnet-cli)

```powershell
dotnet add package TechTolk
```

# [package manager](#tab/package-manager)

```powershell
PM> Install-Package TechTolk
```
---

In the same way, add the [translation set source](./sources/index.md) package(s) of
your liking as well, to be able to load your translations. For example:

# [dotnet CLI](#tab/dotnet-cli)

```powershell
dotnet add package TechTolk.Sources.Resx
dotnet add package TechTolk.Sources.Json
```

# [package manager](#tab/package-manager)

```powershell
PM> Install-Package TechTolk.Sources.Resx
PM> Install-Package TechTolk.Sources.Json
```
---

## Registration

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

> [!TIP]
> If the extension methods like `.AddTechTolk()` are not available, make sure
> you added the required packages to your project and don't forget about the
> `using TechTolk;` statement.

This example uses the built in `CultureInfoDivider`. You will find more
information about dividers at the [dividers](dividers.md) page.

There are shorthand methods and more advanced methods to add your translation
sets. You can also add a translation set that is compiled from multiple sources
into one. More documentation on registering translation sets are on the
[translation sets](translation-sets.md) page.

Adding translation sets can be done with multiple implementations of sources. 
Read more about the possibilities on the [translation set sources](sources/index.md)
page.

## Tolk usage

To actually use TechTolk to render translations from your translation sets, you
need to acquire an `ITolk` instance from your service provider. You can then use
the `.Translate(string)` method to get your translations.

# [in a class or service](#tab/class-or-service)

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

# [in a Razor view](#tab/razor-view)

```
@page
@using TechTolk
@model PrivacyModel
@inject ITolk<MyResxTranslations> Localizer;
@{
    ViewData["Title"] = Localizer.Translate("Pages.Privacy.Title");
}

<h1>@ViewData["Title"]</h1>
<p>@Localizer.Translate("UserGreeting", new { Username = "Fandermill"})</p>
```
---

You can set the behavior of rendering by using an `IValueRenderer`. The built in
renderer features placeholders, but there is also a renderer that uses
[SmartFormat](https://github.com/axuno/SmartFormat) to have more formatting
available in your translations. See the [value renderers](value-renderers.md)
page for more information.
