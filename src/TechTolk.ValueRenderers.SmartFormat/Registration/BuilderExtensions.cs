using Microsoft.Extensions.DependencyInjection;
using SmartFormat;
using TechTolk.Registration.Builders;
using TechTolk.ValueRenderers.SmartFormat;

namespace TechTolk;

public static class BuilderExtensions
{
    /*
     *  TODO - Eliminate the need of the IServiceCollection parameter. We should look into refactoring
     *         the builder so we won't need any services.AddThisAndThatServices() before using them
     *         in the TechTolk registration.
     */


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
