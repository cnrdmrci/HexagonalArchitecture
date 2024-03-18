using System.Net.Http.Headers;
using System.Text;
using AutoFixture;
using AutoFixture.Dsl;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApi;

namespace HexagonalArchitecture.IntegrationTest.Helpers;

public abstract class BaseIntegrationTest<T> where T : RegistrationBase
{
    private Fixture _fixture;
    private IServiceProvider _serviceProvider;
    private RegistrationBase _registrationBase = (RegistrationBase)Activator.CreateInstance(typeof(T));
    protected HttpClient HttpClient { get; private set; }

    [SetUp]
    public void OneTimeSetUp()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {
            builder.ConfigureTestServices(services => {
                Task.FromResult(_registrationBase.ConfigureServicesAsync(services));
            });
        });
        HttpClient = webApplicationFactory.CreateDefaultClient();
        _serviceProvider = webApplicationFactory.Services.CreateScope().ServiceProvider;

        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [TearDown]
    public async Task TearDown()
    {
        HttpClient.Dispose();
        await _registrationBase.StopAsync();
    }
    
    
    protected TType GetRequiredService<TType>()
    {
        return _serviceProvider.GetRequiredService<TType>();
    }
    
    protected void ClearDataContext(ApplicationDbContext applicationDbContext)
    {
        applicationDbContext.Database.EnsureDeleted();
    }
    
    protected ICustomizationComposer<TType> CreateObjectBuilder<TType>()
    {
        return _fixture.Build<TType>();
    }
    
    protected StringContent CreateContentWithGivenBody(object requestBody)
    {
        var body = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }
}