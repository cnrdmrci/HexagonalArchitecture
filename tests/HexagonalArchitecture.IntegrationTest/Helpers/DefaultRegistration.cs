using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public class DefaultRegistration : RegistrationBase
{
    protected override void AddExtraServices(IServiceCollection services)
    {
        RemoveService<ApplicationDbContext>(services);
        services.AddDbContext<ApplicationDbContext>(builder => builder.UseInMemoryDatabase("defaultDatabase"));
        //ReplaceTransientService<IUserService, FakeUserService>(services);
    }

    private void ReplaceScopedService<T, TF>(IServiceCollection services) where T : class where TF : class, T
    {
        RemoveService<T>(services);
        services.AddScoped<T, TF>();
    }
    
    private static void RemoveService<T>(IServiceCollection services) where T : class
    {
        var service = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
        if(service != null)
            services.Remove(service);
    } 
}