using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebApi.Options.SwaggerOptions;

public static class SwaggerOptionExtensions
{
    public static void AddSwaggerRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen();
        serviceCollection.ConfigureOptions<ConfigureSwaggerOptions>();
    }
    
    public static void UseSwaggerConfiguration(this WebApplication app, IServiceCollection serviceCollection)
    {
        var provider = serviceCollection.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}