using Microsoft.Extensions.DependencyInjection;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public abstract class RegistrationBase
{

    public async Task ConfigureServicesAsync(IServiceCollection services)
    {
        await AddExtraServicesAsync(services);
    }

    protected abstract Task AddExtraServicesAsync(IServiceCollection services);
    
    public virtual Task StopAsync()
    {
        return Task.CompletedTask;
    }
    
    protected void ReplaceScopedService<T, TF>(IServiceCollection services) where T : class where TF : class, T
    {
        RemoveService<T>(services);
        services.AddScoped<T, TF>();
    }
    
    protected static void RemoveService<T>(IServiceCollection services) where T : class
    {
        var service = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
        if(service != null)
            services.Remove(service);
    } 

}