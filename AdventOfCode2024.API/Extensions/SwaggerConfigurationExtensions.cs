namespace AdventOfCode2024.API.Extensions;

public static class SwaggerConfigurationExtensions
{
    public static IServiceCollection SwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "AdventOfCode2024 API",
                Description = "An ASP.NET Core Web API for managing Advent of code 2024 quiz",
                Contact = new OpenApiContact
                {
                    Name = "Nicola Palmieri",
                    Email = "npalmieri@hotmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit")
                }
            });

            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}
