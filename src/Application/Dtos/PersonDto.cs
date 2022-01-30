using Domain.AggregateModels.PersonModel;
using Mapster;

namespace Application.Dtos;

public class PersonDto : IRegister
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Person, PersonDto>();
    }
}