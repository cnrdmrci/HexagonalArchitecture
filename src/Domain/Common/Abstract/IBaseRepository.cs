namespace Domain.Common.Abstract;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
 
    TEntity GetById(int id);
    Task<TEntity> GetByIdAsync(int id);
 
    void Add(TEntity entity);
 
    void Update(int id, TEntity entity);
 
    void Delete(int id);
}