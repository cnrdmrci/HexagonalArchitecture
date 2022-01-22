using Domain.Common.Abstract;
using Domain.Common.Models;
using Domain.Port.Repository;
using MapsterMapper;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommandHandler  : IRequestHandler<CreatePersonCommand, ServiceResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ServiceResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Domain.Entities.Person>(request);
        _personRepository.Add(person);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(new CreatePersonCommandViewModel{PersonId = person.Id});
    }
}