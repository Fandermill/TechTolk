# Introduction

You have reached the documentation pages of TechTolk! I hope you will find what
you are looking for. If not, please get in touch. You will find TechTolk on
[GitHub](https://github.com/Fandermill/TechTolk).



## What is TechTolk?

TechTolk is a powerful and flexible .NET localization library with an extensible
set of sources and translation rendering features. It loads translation sets
into memory, registered through a simple-to-use API.

### Features

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



## Why use TechTolk?

So here's the "Commercial" pitch, in which I'm not very good at writing:

### Sources
TechTolk supports multiple implementations of sourcing your translations. You
can load them from [JSON files](./sources/json.md) and 
[resource files](./sources/resx.md). If this doesn't fit your needs, you can
[implement your own](./sources/diy.md).

### Merging multiple sources
You can merge multiple sources into one translation set. This way you can load
your default translations and override them with another source for a specific
context. And it doesn't matter from where the translations come from, so you can
mix source implementations. A use case could be to load the default set from a
database and override them with translation from a local file.

### Value renderers
You can make use of '[value renderers](./value-renderers.md)' to set the
behavior of rendering. By default you can use placeholders in your translations
and TechTolk will replace them with the parameters you gave. Using the
SmartFormat renderer, you can add additional meta data to the placeholders for
additional smart formatting.


### Extensible
And if there is still something missing for your solution, you can always extend
TechTolk with custom made sources and value renderers. I would love to see what
you made!



## Try TechTolk

Want to try TechTolk, but don't want to change all your translation calls in
your views? There is an additional library that you can use as a drop-in
replacement for .NET's localization implementation. See 
[.NET Localization Adapter](net-localization-adapter.md) for more information.



## Support

If you encounter any issues or have questions, please open an issue on the 
[GitHub repository](https://github.com/Fandermill/TechTolk/issues).



## License

This project is licensed under the 
[MIT License](https://raw.githubusercontent.com/Fandermill/TechTolk/main/LICENSE).
Feel free to use, modify, and distribute it in your projects.