using AdventOfCode2024.BusinessLayer.Service;

namespace AdventOfCode2024.API.Extensions;

public static class DependecyInjectionExtensions
{
    public static IServiceCollection DependecyInjection(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IDayOneService>()
            .AddClasses(classes => classes.InExactNamespaceOf<DayOneService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}