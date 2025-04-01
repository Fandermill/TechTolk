# .NET Localization Adapter

TechTolk offers an additional package called `TechTolk.Extensions.Localization`
that contains adapter implementations for the `IStringLocalizer` and the
`IStringLocalizerFactory` interfaces from .NET's
`Microsoft.Extensions.Localization` package. This way, you can still benefit
from localization features from others that depend on the `IStringLocalizer`,
but with TechTolk providing the translations. For example, you can keep using
data annotations for translating property names or validation messages.

You can also use it as a drop-in replacement in an existing project. This won't
require modifying all your views as you can keep using the `IStringLocalizer<T>`
like you would with `Microsoft.Extensions.Localization`. Great for trying out
TechTolk!



## Registering the adapters

Registering the TechTolk adapters will replace the service registration for
`IStringLocalizerFactory`. Simply call `AddTechTolkLocalizationAdapters()` on
your `IServiceCollection`, but make sure you call it _after_ you called
`AddLocalization()`.

Then add your resources that you would normally use for Microsoft's localizer as
translation sets to TechTolk. You can use the
[`TechTolk.Sources.Resx`](./sources/resx.md) package to add translation sets
from resource files, but you are not bound to use resource files as sources. A
[JSON source](./sources/json.md) or you own [custom source](./sources/diy.md)
will do just fine, as long as you use the same tag type `T` to register the
translation set.

Lastly, your configured supported [dividers](./dividers.md) should at least
match the cultures you previously expected. Especially when using the
[`RequestLocalization`](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization/select-language-culture)
middleware in ASP.NET Core.

In the end, the `Program.cs` of a simple ASP.NET Core application could look
like this:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
builder.Services.AddLocalization();

// Call this *after* you call AddLocalization()
builder.Services.AddTechTolkLocalizationAdapters();

// Supported cultures to be used 
// as dividers in TechTolk and 
// as request cultures in RequestLocalization middleware.
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("nl-NL")
};

builder.Services
    .AddTechTolk()

    // Add the supported cultures as dividers
    .UseCultureInfoDividers(supportedCultures)

    // Add the translations from the resouce files bound to
    // the MyAccount razor page
    .AddTranslationSetFromResource<MyAccountModel>()

    // Using a different source is fine too
    .AddTranslationSet<MyOrdersModel>(set => {
        set.FromJson("./Translations/MyOrders.json");
    });

var app = builder.Build();

app.UseRequestLocalization(options => 
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("nl-NL")
    };

    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

/* ... other middleware ... */

app.Run();
```
