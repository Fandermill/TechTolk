using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartFormat;
using TechTolk.Registration.Builders;
using TechTolk.ValueRenderers.SmartFormat;

namespace TechTolk;

public static class BuilderExtensions
{
    public static ITechTolkBuilder UseSmartFormatValueRenderer(this ITechTolkBuilder builder)
    {
        builder.Services.TryAddSingleton<SmartFormatValueRenderer>();
        builder.ConfigureDefaultOptions(c => c.UseValueRenderer<SmartFormatValueRenderer>());
        return builder;
    }

    public static ITechTolkBuilder UseSmartFormatValueRenderer(this ITechTolkBuilder builder, SmartFormatter formatter)
    {
        builder.Services.TryAddSingleton(p => new SmartFormatValueRenderer(formatter));
        builder.ConfigureDefaultOptions(c => c.UseValueRenderer<SmartFormatValueRenderer>());
        return builder;
    }

    public static ITranslationSetOptionsBuilder UseSmartFormatValueRenderer(this ITranslationSetOptionsBuilder builder)
    {
        builder.RootBuilder.Services.TryAddSingleton<SmartFormatValueRenderer>();
        builder.UseValueRenderer<SmartFormatValueRenderer>();
        return builder;
    }
}
