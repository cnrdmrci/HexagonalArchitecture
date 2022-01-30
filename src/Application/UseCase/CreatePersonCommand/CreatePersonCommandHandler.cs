using Application.DomainEvent.Events;
using Application.Ports.Repository;
using Domain.AggregateModels.PersonModel;
using Domain.Common.Abstract;
using Domain.Common.Models;
using MapsterMapper;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommandHandler  : IRequestHandler<CreatePersonCommand, ServiceResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }
    
    public async Task<ServiceResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);
        _personRepository.Add(person);

        await _mediator.Publish(new PersonCreatedDomainEvent() { Username = person.Username }, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(new CreatePersonCommandViewModel{PersonId = person.Id});
    }
}