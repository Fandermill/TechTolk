
# Getting Started

It requires a few steps to get started with TechTolk. First you have to configure 
TechTolk with your DI container and specify which dividers you will support. 
You also have to configure translation sets from which the translation values will 
be returned. You can then use an ITolk<T>, where <T> corresponds to a translation set,
by injecting it into your constructors and calling .Translate("key") where you need it.


## Registration

```csharp
services
    .AddTechTolk()
    .UseCultureInfoDividers("nl-NL", "en-US")

    // (optional) set default behavior
    .WithOptions(o => o.OnTranslationNotFound().ReturnEmtpyString())

    // Add translation set from an embedded resource
    // with the TechTolk.Sources.Resx package
    .AddTranslationSetFromResource<MyResource>()

    // Or add translations from a hardcoded class
    // This time with the more configurable methods
    // and also override the behavior upon 'translation not found'
    .AddTranslationSetFromJson("NamedSet", set => {
        set.FromJson(new MyHardCodedSet());
        set.WithOptions(o => o.OnTranslationNotFound().ThrowException());
    });
```

## Sources

todo - text

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


## Tolk usage

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