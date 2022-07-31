# TechTolk

Test include diagram here:

![Test class diagram svg](docs/draw-io-class-diagram.svg)


## Usage

Makkelijkst om te gebruiken:


Startup

```csharp

builder
  .AddTechTolk()
    .WithCurrentCulture()
    .WithRazorHelper()
  ...
  
app.UseTechTolk(c => {
  c.AddTranslationSet(new EFTranslationSet("connectionstring"));
  c.AddTranslationSet(new JsonTranslationSet("blabla"));
});

```

In razor

```csarp
[Inject]
public ITranslationProvider T {get;}


// bla bla, more work

@T.Translate("MyWebsite.MyAccount.EditPassword")
// (which uses some current culture provider, like Thread.CurrentCulture, if that still exists)


or

@T.Translation("MyWebsite.MyAccount.EditPassword", "nl")
```


Verder
* Prop bij class Translation of het HTML betreft of niet (trust?) Zo ja, dan zou een Razor renderer een new MvcHtmlstring uit moeten spuwen ?
* 'nl' als standaard, maar je zou 'nl-NL' kunnen overriden, want specifieker.
