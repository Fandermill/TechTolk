using System.Globalization;
using Microsoft.Extensions.Logging;
using TechTolk;

namespace Example1;

public sealed class Runner : IExampleRunner
{
    private readonly ITolk<Runner> _tolk;
    private readonly ILogger<Runner> _logger;

    public Runner(ITolk<Runner> tolk, ILogger<Runner> logger)
    {
        _tolk = tolk;
        _logger = logger;
    }

    public void Run()
    {
        CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");

        using (_logger.BeginScope("Running example 1"))
        {
            _logger.LogInformation(
                "Current translation for {CurrentUICulture}: {TranslatedValue}",
                CultureInfo.CurrentUICulture, _tolk.Translate("MyKey"));

            _logger.LogInformation(
                "Translation for en-GB: {TranslatedValue}",
                _tolk.Translate(CultureInfo.GetCultureInfo("en-US"), "MyKey"));
        }
    }
}