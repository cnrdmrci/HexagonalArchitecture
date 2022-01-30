using Domain.AggregateModels.PersonModel;
using Domain.Common.Abstract;

namespace Application.Ports.Repository;

public interface IPersonRepository : IBaseRepository<Person>
{
    
}