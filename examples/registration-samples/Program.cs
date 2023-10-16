using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTolk;
using TechTolk.Division.CultureInfo;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTechTolk()
    .ConfigureDividers(dividers =>
    {
        // TODO - Can we me make overrides/extensions with implicit conversion from string to CultureInfo
        dividers.AddSupportedDivider(CultureInfoDivider.FromCulture("nl-NL"));
        dividers.AddSupportedDivider(CultureInfoDivider.FromCulture("en-US"));
    })
    .AddTranslationSet<Program>(set => set.FromSource(new HardCodedSet()));


var host = builder.Build();

CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");
Console.WriteLine("Current thread ui culture: "+ CultureInfo.CurrentUICulture);
using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var tolk = scope.ServiceProvider.GetRequiredService<ITolk<Program>>();
Console.WriteLine("Translated value: " + tolk.Translate("MyKey"));

await host.RunAsync();




class HardCodedSet : ITranslationSetSource
{
    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder.ForDivider(CultureInfoDivider.FromCulture("nl-NL"))
            .Add(new[]
            {
                ("MyKey","MyValue")
            });
    }
}