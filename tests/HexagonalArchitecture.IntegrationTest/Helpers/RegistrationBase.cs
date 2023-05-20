using Microsoft.Extensions.DependencyInjection;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public abstract class RegistrationBase
{

    public void ConfigureServices(IServiceCollection services)
    {
        AddExtraServices(services);
    }

    protected abstract void AddExtraServices(IServiceCollection services);

}