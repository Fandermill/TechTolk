# TechTolk

TechTolk makes it easy to load translations from any source into memory and use them in your code.
Independent or sources, it can also merge multiple translation sets together, which makes it easy 
to override default translations.

## Getting Started

To use TechTolk in your project, you need to register translation sets before building the service
provider. Then, by injecting an instance of `ITolk<>` or `ITolkFactory`, you can request translated
values.

### Register translation sets

```csharp
services
	.AddTechTolk()
	.ConfigureDividers(config => {
		config.AddSupportedDivider(/*todo*/);
		config.AddSupportedDivider(/*todo*/);
	})
	.AddTranslationSet(
```

### Getting translation values


# Packages

TechTolk
--------
The core package


TechTolk.Extensions.Localization
--------------------------------
Adapters to be used in AspNetCore.
