# Value renderers

A value renderer turns a translated value into a `string`. The `.Translate(...)`
method accepts additional arguments, in which you can pass data for the value
renderer.

## Default renderer

The default value renderer of TechTolk supports the use of placeholders in your
translation values. When passing data as an anonymous object, along with the
`.Translate` call, the property names will be used to replace named
placeholders.

```csharp
// Mykey: {Name} is {Age} years old
var result = _tolk.Translate("MyKey", new { Name = "Peter", Age = 42 });
// result: Peter is 42 years old
```


When passing in data as an array, the values will be merged in order.

```csharp
var data = new object[] { "Peter", 42 };
// MyKey: {0} is {1} years old
var result = _tolk.Translate("MyKey", data);
// result: Peter is 42 years old
```


## SmartFormat value renderer

If you need more flexibility in rendering your translation values, there is an
extension package `TechTolk.ValueRenderers.SmartFormat` that leverages Axuno's
[SmartFormat](https://github.com/axuno/SmartFormat). Check it's Github repo for
the countless possibilities.

```csharp
// With SmartFormat, you can turn this translation:
// MyKey: "You have {Count:plural:no messages|a new message|multiple messages}"
var result = _tolk.Translate("MyKey", new { Count = 1 });
// into this result:
// "You have a new message"
```

Make sure your project references the `TechTolk.ValueRenderers.SmartFormat`
package and call `.UseSmartFormatValueRenderer()` on the `ITolkTolkBuilder`.
Pass in an optional `SmartFormatter` object to set the SmartFormat behavior.
See the [SmartFormat's documentation](https://github.com/axuno/SmartFormat/wiki/Configuration)
for information on it's configuration.

```csharp
services
    .AddTechTolk()
    // Use as default renderer
    .UseSmartFormatValueRenderer()
    .AddTranslationSet("MySet", set => {
        //set.FromSource(...)

        // Or use for this specific translation set
        set.WithOptions(o => o.UseSmartFormatValueRenderer());
    });
```

## Roll your own

If you need behavior that isn't covered by the existing renderers, you could
roll your own. You do this by overriding the `AbstractTranslationValueRenderer`,
which resided in the `TechTolk.Renderers` namespace. The abstract class forces
you to implement a simple method, which should be pretty self explanatory.

You then need to register the type of your value renderer with TechTolk. You can
do this at the default level, or at a specific translation set level. Don't
forget to register your renderer in your DI container.

```csharp
services
    .AddTechTolk()

    //.ConfigureDividers(...)

    // Configure your value renderer as the default
    .ConfigureDefaultOptions(o => o.UseValueRenderer<MyValueRenderer>())

    // and/or configure a value renderer for a specific translation set
    .AddTranslationSet("MySet", set => {
        //set.FromSource...
        set.WithOptions(o => o.UseValueRenderer<MySpecificValueRenderer>());
    }));
```
