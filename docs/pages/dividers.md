
# Dividers

A divider is an identifier that divides multiple sets of translation values within
a translation set. The most common divider would be a `CultureInfo` so you can have
translated values for different cultures. But you can use anything really, because
it results in a `string` key, as long as it implements the `IDivider` interface.

A `ICurrentDividerProvider` implementation is used to provide the divider to be used
if no divider is given when requesting a translation value.

## Specifying supported dividers

When registering `TechTolk` with your DI container, you have to specify which dividers
you want to support. Building translation sets as well as requested translation values
will only be possible with supported dividers.

To specify the supported dividers, you should use the `IDividerConfigurationBuilder`
when calling `.ConfigureDividers(Action<IDividerConfigurationBuilder>)`.

```csharp

services
    .AddTechTolk()
    .ConfigureDividers(config => {
        config.AddSupportedDivider(new CultureInfo("nl-NL"));
        config.AddSupportedDivider(new CUltureInfo("en-US"));
    });
```

> [!WARNING]
> **TODO** We should make it as easy as possible to get started. Maybe add the supported dividers into
> the AddTechTolk() method, like services.AddTechTolk(Div1, Div2, Div3).

> [!WARNING]
> **TODO** Say something about the implicit operator override to convert a CultureInfo into
> the built in CultureInfoDivider.

## The `ICurrentDividerProvider`

When a translation value is requested without specifying an `IDivider`, the configured
implementation of `ICurrentDividerProvider` is used to get a divider to use for translating
the value.

By default, an internal provider is used that will give you the current `CultureInfo` from the
current UI thread. But you can use your own by calling `.SetCurrentDividerProvider()`.

```csharp
services
    .AddTechTolk()
    .ConfigureDividers(config => {
        
        // ...

        // Register your type as a singleton
        config.SetCurrentDividerProvider<MyCurrentDividerProvider>();

        // or you can register your instance with a factory method
        config.SetCurrentDividerProvider((serviceProvider) => {
            return new MyCurrentDividerProvider(serviceProvider.GetRequiredService<MyDependentService>());
        });
    });
```

*todo should we only refer to the API doc to implement the interface? or make an actuel manual?*