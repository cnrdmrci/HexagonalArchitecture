using Testcontainers.MsSql;

namespace HexagonalArchitecture.IntegrationTest.Containers;

public class MssqlTestContainer
{
    private readonly MsSqlContainer _msSqlContainer;
    
    public MssqlTestContainer()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();
    }

    public string GetConnectionString()
    {
        return _msSqlContainer.GetConnectionString();
    }
    
    public async Task StartAsync()
    {
        await _msSqlContainer.StartAsync();
    }
    
    public async Task StopAsync()
    {
        await _msSqlContainer.StopAsync();
        await _msSqlContainer.DisposeAsync();
    }
    
}