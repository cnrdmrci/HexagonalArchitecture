using Domain.Entities;
using Domain.Port.Repository;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Adapter.Repository;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context) { }
}