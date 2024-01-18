using System.Globalization;
using Microsoft.Extensions.Logging;
using TechTolk;

namespace registration_samples;

public sealed class Example2Runner : IExampleRunner
{
    private readonly ITolk<Example2Runner> _tolk;
    private readonly ILogger<Example2Runner> _logger;

    public Example2Runner(ITolk<Example2Runner> tolk, ILogger<Example2Runner> logger)
    {
        _tolk = tolk;
        _logger = logger;
    }

    public void Run()
    {
        CultureInfo.CurrentUICulture = new CultureInfo("nl-NL");

        using (_logger.BeginScope("Running example 2"))
        {
            _logger.LogInformation(
                "Current translation for {CurrentUICulture}: {TranslatedValue}",
                CultureInfo.CurrentUICulture, _tolk.Translate("SetA-TranslationKey"));

            _logger.LogInformation(
                "Translation for en-US: {TranslatedValue}",
                _tolk.Translate(CultureInfo.GetCultureInfo("en-US"), "SetA-TranslationKey"));

            _logger.LogInformation(
                "Translation for key that does not exist (en-US): {TranslatedValue}",
                _tolk.Translate(CultureInfo.GetCultureInfo("en-US"), "ThisKey.DoesNot.Exist"));
        }
    }
}