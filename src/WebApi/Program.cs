using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Common.Filters;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using WebApi.Options.ApiVersionOptions;
using WebApi.Options.SwaggerOptions;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Registrations
        
        builder.AddInfrastructureRegistration();
        
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        builder.Services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);
        
        builder.Services.AddApiVersioningRegistration();

        builder.Services.AddSwaggerRegistration();
        
        #endregion

        var app = builder.Build();
        
        // app.UseOpenTelemetryPrometheusScrapingEndpoint();

        app.UseMiddleware<ResponseTraceIdMiddleware>();
        
        app.UseSwaggerConfiguration(builder.Services);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}

