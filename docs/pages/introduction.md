# Introduction

You have reached the documentation pages of TechTolk! I hope you will find what
you are looking for. If not, please get in touch. You will find TechTolk on
[GitHub](https://github.com/Fandermill/TechTolk).



## What is TechTolk?

TechTolk is a .NET localization library with an extensible set of sources and 
translation rendering features. It loads translations sets into memory, registered
by a simple to use API.



## Why use TechTolk?

So here's the "Commercial" pitch, in which I'm not very good at writing:

### Sources
TechTolk supports multiple implementations of sourcing your translations. At the
moment, you can hard code it, or load them from (embedded) resource files, like
Microsoft does. More implementations are on the way!

### Merging multiple sources
You can merge multiple sources into one translation set. This way you can load
your default translations and override them with another source for a specific
context. And it doesn't matter from where the translations come from, so you
can mix source implementations. A use case could be to load the default set from
a database and override them with translation from a local file.

### Value renderers
You can make use of 'value renderers' to set the behavior of rendering. By default
you can use placeholders in your translations and TechTolk will replace them with
the parameters you gave. Using the SmartFormat renderer (to be implemented), you
can add additional meta data to the placeholders for additional smart formatting.


### Extensible
And if there is still something missing for your solution, you can always extend
TechTolk with custom made sources and value renderers. I would love to see what
you made!



## Try TechTolk

Want to try TechTolk, but don't want to change all your translation calls in your
views? There is a additional library that you can use as a drop in for .NET's
localization implementation. See [TODO_LINK](#try-techtolk) for more information.



## Why I made it

We already had our own localization/translation system implemented when we switched
from .NET Framework to .NET 6 at the company I work for. It was fairly old and I 
don't know if you have ever looked at code you wrote years ago, but chances are 
that you are absolutely shocked by the way you were coding back then. Well, it was
like that. And because it had a dependency on System.Web, which is not supported
from .NET Core, I thought it was a good moment to rewrite it.

Following the Microsoft guide to implement localization using `Microsoft.Extensions.Localization`,
there only seemed to be one implementation, resource files, built in to use as a 
source of the translations. And we needed the ability to have a default set from
a database and also the feature to override specific translations in different
customer contexts. Another reason to built my own library.
