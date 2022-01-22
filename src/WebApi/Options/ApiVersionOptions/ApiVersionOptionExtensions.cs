using Microsoft.AspNetCore.Mvc;

namespace WebApi.Options.ApiVersionOptions;

public static class ApiVersionOptionExtensions
{
    public static void AddApiVersioningRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddApiVersioning(config =>
        {
            // Specify the default API Version as 1.0
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // If the client hasn't specified the API version in the request, use the default API version number 
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });

        serviceCollection.AddVersionedApiExplorer(options =>  
        {  
            //The format of the version added to the route URL  
            options.GroupNameFormat = "'v'VVV";  
            //Tells swagger to replace the version in the controller route  
            options.SubstituteApiVersionInUrl = true;  
        });
    }
}