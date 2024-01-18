using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using registration_samples;
using TechTolk;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(new MyCustomValueRenderer());
builder.Services.AddTechTolk()
    .ConfigureDividers(dividers =>
    {
        // Register the supported dividers here
        dividers
            .AddSupportedCultureInfoDivider("nl-NL")
            .AddSupportedCultureInfoDivider("en-US");
    })

    // Example 1 - Simple hard coded translation set
    .AddTranslationSet<Example1Runner>(set => set.FromSource(new HardCodedSetA()))

    // Example 2 - Translation set options
    .AddTranslationSet<Example2Runner>(set =>
    {
        set.FromSource<HardCodedSetA>();
        set.WithOptions(o => o

            // The translation set will lazy load upon the first
            // time a translation is requested from the translation set
            .OnTranslationSetNotLoaded().LazyLoad()

            // When a required translation key is not present,
            // return the requested key as the value
            .OnTranslationNotFound().ReturnTranslationKey()

            // Use a custom value renderer
            .UseValueRenderer<MyCustomValueRenderer>());
    })

    // Example 3 - Merged translation sets
    .AddMergedTranslationSet<Example3Runner>(mergedSet =>
    {
        // When a translation key exists in A and B, the value from
        // the latter translation set takes precedence over previous ones
        mergedSet.WithOptions(o => o.OnDuplicateKey().Replace());

        mergedSet.FromSource<HardCodedSetA>("SetA");
        mergedSet.FromSource<HardCodedSetB>("SetB");
    });


builder.Services.AddScoped<Example1Runner>();
builder.Services.AddScoped<Example2Runner>();
builder.Services.AddScoped<Example3Runner>();

var host = builder.Build();

host.RunExampleInScope<Example1Runner>();
host.RunExampleInScope<Example2Runner>();
host.RunExampleInScope<Example3Runner>();

//await host.RunAsync();