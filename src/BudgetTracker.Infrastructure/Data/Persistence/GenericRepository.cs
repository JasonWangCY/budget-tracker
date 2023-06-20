using BudgetTracker.Domain.PersistenceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class GenericRepository<TClass> : IGenericRepository<TClass> where TClass : class, IAggregateRoot
{
    internal DbSet<TClass> dbSet;

    public GenericRepository(BudgetTrackerDbContext dbContext)
    {
        dbSet = dbContext.Set<TClass>();
    }

    public virtual async Task<TClass?> GetByIdAsync(string id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(TClass entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TClass> entities)
    {
        await dbSet.AddRangeAsync(entities);
    }
}
