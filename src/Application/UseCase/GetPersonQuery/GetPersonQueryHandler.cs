using Application.Dtos;
using Application.Ports.Repository;
using Domain.Common.Models;
using MapsterMapper;
using MediatR;

namespace Application.UseCase.GetPersonQuery;

public class GetPersonQueryHandler(
    IPersonRepository personRepository, 
    IMapper mapper) : IRequestHandler<GetPersonQuery, ServiceResult>
{
    public async Task<ServiceResult> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetByIdAsync(request.Id);
        var dto = mapper.Map<PersonDto>(person);
        var personVm = new GetPersonViewModel { PersonDto = dto };

        return ServiceResult.Success(personVm);
    }
}