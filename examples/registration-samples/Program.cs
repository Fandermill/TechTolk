using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTolk;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTechTolk()
    .ConfigureDividers(dividers =>
    {
        // Register the supported dividers here
        dividers
            .AddSupportedCultureInfoDivider("nl-NL")
            .AddSupportedCultureInfoDivider("en-US");
    })

    // Example 1 - Simple hard coded translation set
    .AddTranslationSet<Example1.Runner>(set => set.FromSource(new Example1.HardCodedSet()))

    // Example 2 - Translation set options
    .AddTranslationSet<Example2.Runner>(set =>
    {
        set.FromSource(new Example2.HardCodedSet());
        set.WithOptions(o => o
            // When a required translation key is not present,
            // return the requested key as the value
            .OnTranslationNotFound().ReturnTranslationKey()
            
            // Use a custom value renderer
            .UseValueRenderer<object>());
    })

    // Example 3
    .AddMergedTranslationSet<Example3.Runner>(mergedSet =>
    {
        mergedSet.WithOptions(o => o
            .OnDuplicateKey().
    })

    .AddTranslationSet<Example2.Runner>(set =>
    {

    })


builder.Services.AddScoped<Example1.Runner>();

var host = builder.Build();

host.RunExampleInScope<Example1.Runner>();

//await host.RunAsync();





public static class ExampleRunnerExtensions
{
    public static void RunExampleInScope<T>(this IHost host) where T : IExampleRunner
    {
        using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var exampleRunner = scope.ServiceProvider.GetRequiredService<T>();
        exampleRunner.Run();
    }
}