using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTolk;
using TechTolk.Division.String;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTechTolk()
    .ConfigureDividers(dividers =>
    {
        dividers.AddSupportedDivider("nl");
        dividers.AddSupportedDivider("en");
    })
    .AddTranslationSet<Program>(set => set.FromSource(new HardCodedSet()));


var host = builder.Build();

using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var tolk = scope.ServiceProvider.GetRequiredService<ITolk<Program>>();
Console.WriteLine("Translated value: " + tolk.Translate(new StringDivider("nl"), "MyKey"));

await host.RunAsync();




class HardCodedSet : ITranslationSetSource
{
    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(new StringDivider("nl"))
            .Add(new[]
            {
                ("MyKey","MyValue")
            });
    }
}