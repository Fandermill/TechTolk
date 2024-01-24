> [!WARNING]
> The package and it's documentation are a work-in-progress. All of this is subject to change!

# TechTolk

TechTolk makes it easy to load translations from any source into memory and use them in your code.
Independent of sources, it can also merge multiple translation sets together, which makes it easy 
to override default translations.

## Getting Started

To use TechTolk in your project, you need to register translation sets before building the service
provider. Then, by injecting an instance of `ITolk<>` or `ITolkFactory`, you can request translated
values.

First thing to do is adding TechTolk to the service container by calling `services.AddTechTolk()`.
Then you need to configure the supported [dividers](#Dividers). You can do this like so:

```csharp
services
	.AddTechTolk()
	.ConfigureDividers(config => {
		config
			.AddSupportedCultureInfoDivider("nl-NL")
			.AddSupportedCultureInfoDivider("en-US");
	});
```

When requesting a translation without a divider parameter, the UICulture of the current thread is
used. You can use your own `ICurrentDividerProvider` if you wish. See the documentation for more 
details.

Then you need to add [translation sets](#Register-translation-sets) to the registration that provide the
key-value pairs for every divider.

### Register translation sets

You can register translation sets from a single source, or merge multiple into one. A translation set
source populates the translation sets. This could be with values from a database, a resource file
or a hardcoded class file.

You could register a translation set, accessible from an `ITolk<MyResourceTag>`, with a hard coded
translation set like so:

```csharp
ITechTolkBuilder // (chain with .AddTechTolk())
	.AddTranslationSet<MyResourceTag>(set => set.FromSource(new MyHardCodedTranslationSet());
```

And the source implementation:

```csharp
public class MyHardCodedTranslationSet : ITranslationSetSource
{
    public Task PopulateTranslationsAsync(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            .ForDivider("nl-NL").Add(new[] {
                ( "MyProfile", "Mijn profiel" ),
                ( "UserGreeting", "Hallo {Username}")
            })
            .ThenForDivider("en-US").Add(new[] {
                ( "MyProfile", "My profile" ),
                ( "UserGreeting", "Hello {Username}!")
            });

        return Task.CompletedTask;
    }
}
```


### Getting translation values

You can get the translation values by injecting an `ITolk<T>` into your class. When using the
`Translate` method without a divider parameter, the default divider will be used. Optionally,
you can call `Translate` with a value bag to fill in the runtime values into the translation.

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


# Packages

TechTolk
--------
The core package


TechTolk.Extensions.Localization
--------------------------------
Adapters to be used in AspNetCore.
