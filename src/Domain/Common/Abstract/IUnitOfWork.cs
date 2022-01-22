namespace Domain.Common.Abstract;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task RollbackAsync();
    Task<int> SaveChangesAsync();
    Task CommitAsync();

}