using TechTolk.Division.CultureInfo;
using TechTolk.Registration;
using TechTolk.Sources;
using TechTolk.TranslationSets.Building;

namespace Example1;

public sealed class HardCodedSet : ITranslationSetSource
{
    public void PopulateTranslations(ITranslationSetBuilder builder, SourceRegistrationBase sourceRegistration)
    {
        builder
            
            .ForDivider(CultureInfoDivider.FromCulture("nl-NL"))
            .Add(new[]
            {
                ("MyKey", "My Dutch Value")
            })
            
            .ThenForDivider(CultureInfoDivider.FromCulture("en-US"))
            .Add(new[] {
                ("MyKey", "My English Value")
            });
    }
}