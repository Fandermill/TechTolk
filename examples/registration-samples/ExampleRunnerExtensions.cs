using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class ExampleRunnerExtensions
{
    public static void RunExampleInScope<T>(this IHost host) where T : IExampleRunner
    {
        using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var exampleRunner = scope.ServiceProvider.GetRequiredService<T>();
        exampleRunner.Run();
    }
}