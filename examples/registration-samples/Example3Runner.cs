using System.Globalization;
using Microsoft.Extensions.Logging;
using TechTolk;

namespace registration_samples;

public sealed class Example3Runner : IExampleRunner
{
    private readonly ITolk<Example3Runner> _tolk;
    private readonly ILogger<Example3Runner> _logger;

    public Example3Runner(ITolk<Example3Runner> tolk, ILogger<Example3Runner> logger)
    {
        _tolk = tolk;
        _logger = logger;
    }

    public void Run()
    {
        CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");

        using (_logger.BeginScope("Running example 3"))
        {
            _logger.LogInformation(
                "Current translation for {CurrentUICulture}: {TranslatedValue}",
                CultureInfo.CurrentUICulture, _tolk.Translate("KeyInAllSets"));

            _logger.LogInformation(
                "Translation for en-GB: {TranslatedValue}",
                _tolk.Translate(CultureInfo.GetCultureInfo("en-US"), "KeyInAllSets"));
        }
    }
}