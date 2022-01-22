using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApi.Options.ApiVersionOptions;
using WebApi.Options.SwaggerOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureRegistration();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(options =>
        options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

builder.Services.AddApiVersioningRegistration();
builder.Services.AddSwaggerRegistration();

var app = builder.Build();

app.UseSwaggerConfiguration(builder.Services);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();