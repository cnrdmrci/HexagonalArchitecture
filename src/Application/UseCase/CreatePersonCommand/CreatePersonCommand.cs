using Domain.AggregateModels.PersonModel;
using Domain.Common.Models;
using Mapster;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommand : IRequest<ServiceResult>, IRegister
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePersonCommand,Person>()
            .Map(x => x.Username, 
                x => x.Name.ToLower() + "_" + x.Surname.ToLower());
    }
}