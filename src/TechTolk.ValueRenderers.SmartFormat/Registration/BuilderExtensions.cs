using Microsoft.Extensions.DependencyInjection;
using SmartFormat;
using TechTolk.Registration.Builders;
using TechTolk.ValueRenderers.SmartFormat;

namespace TechTolk;

public static class BuilderExtensions
{
    public static ITechTolkBuilder UseSmartFormatValueRenderer(this ITechTolkBuilder builder, IServiceCollection services)
    {
        services.AddSingleton<SmartFormatValueRenderer>();
        builder.ConfigureDefaultOptions(c => c.UseValueRenderer<SmartFormatValueRenderer>());
        return builder;
    }

    public static ITechTolkBuilder UseSmartFormatValueRenderer(this ITechTolkBuilder builder, IServiceCollection services, SmartFormatter formatter)
    {
        services.AddSingleton(p => new SmartFormatValueRenderer(formatter));
        builder.ConfigureDefaultOptions(c => c.UseValueRenderer<SmartFormatValueRenderer>());
        return builder;
    }
}
