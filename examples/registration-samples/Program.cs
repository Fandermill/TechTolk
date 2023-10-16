using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTolk;
using TechTolk.Division.CultureInfo;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTechTolk()
    .ConfigureDividers(dividers =>
    {
        // TODO - Can we me make overrides/extensions with implicit conversion from string to CultureInfo
        dividers.AddSupportedDivider(CultureInfoDivider.FromCulture("nl-NL"));
        dividers.AddSupportedDivider(CultureInfoDivider.FromCulture("en-US"));
    })

    // Example 1 - Simple hard coded translation set
    .AddTranslationSet<Example1.Runner>(set => set.FromSource(new Example1.HardCodedSet()));


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