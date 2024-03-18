using HexagonalArchitecture.IntegrationTest.Containers;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public class TestContainerRegistration : RegistrationBase
{
    private MssqlTestContainer _msSqlTestContainer;
    
    protected override async Task AddExtraServicesAsync(IServiceCollection services)
    {
        var mssqlTestContainerTask = AddMssqlTestContainer(services);
        
        await Task.WhenAll(mssqlTestContainerTask);
    }

    private async Task AddMssqlTestContainer(IServiceCollection services)
    {
        _msSqlTestContainer = new MssqlTestContainer();
        await _msSqlTestContainer.StartAsync();
        RemoveService<ApplicationDbContext>(services);
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(_msSqlTestContainer.GetConnectionString()));
    }
    
    public override async Task StopAsync()
    {
        await _msSqlTestContainer.StopAsync();
    }

    
}