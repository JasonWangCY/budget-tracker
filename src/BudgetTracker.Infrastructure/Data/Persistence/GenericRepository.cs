using BudgetTracker.Domain.PersistenceInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class GenericRepository<TClass> : IGenericRepository<TClass> where TClass : class
{
    internal DbSet<TClass> dbSet;

    public GenericRepository(BudgetTrackerDbContext dbContext)
    {
        dbSet = dbContext.Set<TClass>();
    }

    public virtual async Task<TClass?> GetById(string id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task Add(TClass entity)
    {
        await dbSet.AddAsync(entity);
    }
}
