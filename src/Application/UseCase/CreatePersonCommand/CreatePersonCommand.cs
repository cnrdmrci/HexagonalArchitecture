using Domain.AggregateModels.PersonModel;
using Domain.Common.Models;
using Mapster;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public record CreatePersonCommand : IRequest<ServiceResult>, IRegister
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public int Age { get; init; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePersonCommand,Person>()
            .Map(x => x.Username, 
                x => x.Name.ToLower() + "_" + x.Surname.ToLower());
    }
}