using Application.DomainEvent.Events;
using Application.Ports.Repository;
using Domain.AggregateModels.PersonModel;
using Domain.Common.Abstract;
using Domain.Common.Models;
using MapsterMapper;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommandHandler(
    IPersonRepository personRepository, 
    IUnitOfWork unitOfWork, 
    IMediator mediator, 
    IMapper mapper) : IRequestHandler<CreatePersonCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = mapper.Map<Person>(request);
        personRepository.Add(person);

        await mediator.Publish(new PersonCreatedDomainEvent { Username = person.Username }, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(new CreatePersonCommandViewModel{PersonId = person.Id});
    }
}