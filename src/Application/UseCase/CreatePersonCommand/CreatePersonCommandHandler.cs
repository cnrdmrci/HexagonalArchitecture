using System.Diagnostics;
using Application.DomainEvent.Events;
using Application.OpenTelemetry;
using Application.Ports.Repository;
using Domain.AggregateModels.PersonModel;
using Domain.Common.Abstract;
using Domain.Common.Models;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommandHandler(
    ILogger<CreatePersonCommandHandler> logger,
    IPersonRepository personRepository, 
    IUnitOfWork unitOfWork, 
    IMediator mediator, 
    IMapper mapper) : IRequestHandler<CreatePersonCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        using var activity = ActivitySourceProvider.Source.StartActivity(
            kind:ActivityKind.Server, name: nameof(CreatePersonCommandHandler));
        activity?.AddEvent(new ActivityEvent("Create user process started.") );

        await TestActivityWaitMethod();
        await TestHttpApiCall(cancellationToken);
        
        var person = mapper.Map<Person>(request);
        personRepository.Add(person);

        await mediator.Publish(new PersonCreatedDomainEvent { Username = person.Username }, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        
        OpenTelemetryMetric.UserCreatedEventCounter.Add(1,
            new KeyValuePair<string, object?>("event","add"));
        
        activity?.SetTag("user id", person.Id);
        activity?.AddEvent(new ActivityEvent("User created.") );
        
        logger.LogInformation("user created.");

        return ServiceResult.Success(new CreatePersonCommandViewModel{PersonId = person.Id});
    }

    private async Task TestActivityWaitMethod()
    {
        using var activity = ActivitySourceProvider.Source.StartActivity();
        activity?.AddEvent(new ActivityEvent("Wait for 2 second"));
        await Task.Delay(2000);
    }

    private async Task TestHttpApiCall(CancellationToken cancellationToken)
    {
        using var activity = ActivitySourceProvider.Source.StartActivity();
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1",cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
    }
}