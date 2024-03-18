using System.Net;
using Application.UseCase.CreatePersonCommand;
using Application.UseCase.GetPersonQuery;
using AutoFixture;
using Domain.AggregateModels.PersonModel;
using HexagonalArchitecture.IntegrationTest.Helpers;
using Infrastructure.Persistence.Context;
using MapsterMapper;

namespace HexagonalArchitecture.IntegrationTest.WebApi;

public class PersonControllerTest : BaseIntegrationTest<TestContainerRegistration>
{
    private const string Route = "api/v1/persons";
    private ApplicationDbContext _applicationDbContext;
    
    [SetUp]
    public void SetUp() 
    {
        _applicationDbContext = GetRequiredService<ApplicationDbContext>();
    }
    
    [TearDown]
    public void TearDown()
    {
        _applicationDbContext.Dispose();
    }
    
    [Test]
    public async Task GetPersonAsync_TrueStory()
    {
        //Arrange
        var createPersonCommand = CreateObjectBuilder<CreatePersonCommand>().Create();
        var mapper = GetRequiredService<IMapper>();
        var person = mapper.Map<Person>(createPersonCommand);

        _applicationDbContext.Persons.Add(person);
        await _applicationDbContext.SaveChangesAsync();
        
        // var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        // httpRequestMessage.Headers.Add("api-version","1");
        //var httpResponse = await httpClient.SendAsync(httpRequestMessage);
        
        //Act
        var requestUri = Route + "/" + person.Id;
        var httpResponse = await HttpClient.GetAsync(requestUri);
        var response = await httpResponse.DeserializeAsync<GetPersonViewModel>();
        
        //Verify
        Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response,Is.Not.Null);
        Assert.That(response?.Succeeded, Is.True);
        Assert.That(person.Name, Is.EqualTo(response?.Data?.PersonDto.Name));
    }
    
    [Test]
    public async Task CreatePersonAsync_TrueStory()
    {
        //Arrange
        var createPersonCommand = CreateObjectBuilder<CreatePersonCommand>().Create();

        //Act
        var httpResponse = await HttpClient.PostAsync(Route,CreateContentWithGivenBody(createPersonCommand));
        var response = await httpResponse.DeserializeAsync<CreatePersonCommandViewModel>();
            
        //Verify
        var person = await _applicationDbContext.Persons.FindAsync(response?.Data?.PersonId ?? 0);
        Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response,Is.Not.Null);
        Assert.That(response?.Succeeded, Is.True);
        Assert.That(person?.Id, Is.EqualTo(response?.Data?.PersonId));

    }
}