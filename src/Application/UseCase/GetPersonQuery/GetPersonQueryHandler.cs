using Application.Dtos;
using Domain.Common.Models;
using Domain.Port.Repository;
using MapsterMapper;
using MediatR;

namespace Application.UseCase.GetPersonQuery;

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, ServiceResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResult> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        var dto = _mapper.Map<PersonDto>(person);
        var personVm = new GetPersonViewModel { PersonDto = dto };

        return ServiceResult.Success(personVm);
    }
}