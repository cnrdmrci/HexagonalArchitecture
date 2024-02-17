using Domain.AggregateModels.PersonModel;
using Mapster;

namespace Application.Dtos;

public record PersonDto : IRegister
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Username { get; init; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Person, PersonDto>();
    }
}