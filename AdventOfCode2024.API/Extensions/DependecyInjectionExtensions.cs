using AdventOfCode2024.BusinessLayer.Service;

namespace AdventOfCode2024.API.Extensions;

public static class DependecyInjectionExtensions
{
    public static IServiceCollection DependecyInjection(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IAdventOfCode2024Service>()
            .AddClasses(classes => classes.InExactNamespaceOf<AdventOfCode2024Service>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}