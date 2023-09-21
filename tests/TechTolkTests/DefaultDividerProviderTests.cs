using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TechTolk;
using TechTolk.Division;

namespace TechTolkTests;

public class DefaultDividerProviderTests
{
    [Fact]
    public void The_default_divider_provider_uses_the_current_thread_ui_culture_info()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTechTolk();
        var provider = serviceCollection.BuildServiceProvider();

        var sut = provider.GetRequiredService<ICurrentDividerProvider>();

        Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");
        var currentDivider = sut.GetCurrent();

        currentDivider.Key.Should().Be("nl");
    }
}