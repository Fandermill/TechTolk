## About the author

Hello, my name is Folkert van der Molen. I am a .NET/C# developer from The 
Netherlands. I started writing code when the HTML tags `<marquee>` and `<blink>`
were still around and I'm programming professionally since 2010.



## Why I made TechTolk

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