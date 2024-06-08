using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TechTolk;
using TechTolk.Division;

namespace TechTolk.Tests;

public class DefaultDividerProviderTests
{
    [Fact]
    public void The_default_divider_provider_uses_the_current_thread_ui_culture_info()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTechTolk();
        var provider = serviceCollection.BuildServiceProvider();
        var culture = new CultureInfo("nl-NL");
        var sut = provider.GetRequiredService<ICurrentDividerProvider>();

        Thread.CurrentThread.CurrentUICulture = culture;
        var currentDivider = sut.GetCurrent();

        currentDivider.Key.Should().Be(culture.Name);
    }
}