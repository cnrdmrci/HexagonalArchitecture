using Domain.Common.Abstract;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}