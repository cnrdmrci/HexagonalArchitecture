using System.Reflection;
using Application.Ports.Repository;
using Application.Services.Common;
using Domain.Common.Abstract;
using Infrastructure.Common.MediatRBehaviours;
using Infrastructure.Persistence.Adapter.Repository;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;
using Infrastructure.Services;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddEntityFrameworkRegistration();
        builder.Services.AddMediatRRegistration();
        builder.Services.AddRepositoriesRegistration();
        builder.Services.AddServicesRegistration();
        builder.Services.AddMapsterRegistration();
        builder.AddOpenTelemetryRegistration();
    }
    
    private static void AddEntityFrameworkRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("inMemoryDb"));
    }

    private static void AddRepositoriesRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
    }

    private static void AddServicesRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDateTime, DateTimeService>();
        serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
    }
    
    private static void AddMediatRRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(IDateTime).GetTypeInfo().Assembly));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
    }
    
    private static void AddMapsterRegistration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(GetConfiguredMappingConfig());
        serviceCollection.AddScoped<IMapper, ServiceMapper>();
    }
    
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var assembly = Assembly.GetAssembly(typeof(IDateTime));
        var config = TypeAdapterConfig.GlobalSettings;

        IList<IRegister> registers = config.Scan(assembly);

        config.Apply(registers);

        return config;
    }
}