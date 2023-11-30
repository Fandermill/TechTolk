# TechTolk

TechTolk makes it easy to load translations from any source into memory and use them in your code.
Independent or sources, it can also merge multiple translation sets together, which makes it easy 
to override default translations.

## Getting Started

To use TechTolk in your project, you need to register translation sets before building the service
provider. Then, by injecting an instance of `ITolk<>` or `ITolkFactory`, you can request translated
values.

First thing to do is adding TechTolk to the service conainer by calling `services.AddTechTolk()`.
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

Then you need to add [translation sets](#Translation-sets) to the registration that provide the
key-value pairs for every divider.

### Register translation sets

```csharp
ITechTolkBuilder // (chain with .AddTechTolk())
	.AddTranslationSet<SomeType>(set => set.FromSource(new SomeTranslationSetSource());
```

### Getting translation values


# Packages

TechTolk
--------
The core package


TechTolk.Extensions.Localization
--------------------------------
Adapters to be used in AspNetCore.
