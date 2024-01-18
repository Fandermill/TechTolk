using System.Globalization;
using Microsoft.Extensions.Logging;
using TechTolk;

namespace registration_samples;

public sealed class Example1Runner : IExampleRunner
{
    private readonly ITolk<Example1Runner> _tolk;
    private readonly ILogger<Example1Runner> _logger;

    public Example1Runner(ITolk<Example1Runner> tolk, ILogger<Example1Runner> logger)
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
                CultureInfo.CurrentUICulture, _tolk.Translate("SetA-TranslationKey"));

            _logger.LogInformation(
                "Translation for en-US: {TranslatedValue}",
                _tolk.Translate(CultureInfo.GetCultureInfo("en-US"), "SetA-TranslationKey"));
        }
    }
}