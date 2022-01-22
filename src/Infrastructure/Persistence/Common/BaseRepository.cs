using Domain.Common.Abstract;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Common;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected ApplicationDbContext Context;
    private DbSet <TEntity> entities;  

    public BaseRepository(ApplicationDbContext context)
    {
        this.Context = context;
        entities = context.Set<TEntity>(); 
    }
    public IQueryable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().AsNoTracking();
    }

    public TEntity GetById(int id)
    {
        return entities.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }
    
    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Add(TEntity entity)
    {
        entities.Add(entity);
    }
 
    public void Update(int id, TEntity entity)
    {
        entities.Update(entity);
    }
 
    public void Delete(int id)
    {
        var entity = GetById(id);
        entities.Remove(entity);
    }
}