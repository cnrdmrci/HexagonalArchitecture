using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public class DefaultRegistration : RegistrationBase
{
    protected override Task AddExtraServicesAsync(IServiceCollection services)
    {
        RemoveService<ApplicationDbContext>(services);
        services.AddDbContext<ApplicationDbContext>(builder => 
            builder.UseInMemoryDatabase("defaultDatabase"));
        
        return Task.CompletedTask;
    }
}