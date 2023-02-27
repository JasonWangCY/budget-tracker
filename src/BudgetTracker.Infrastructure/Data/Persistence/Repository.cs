using BudgetTracker.Domain.PersistenceInterfaces;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class Repository : IRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public Repository(BudgetTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add<TClass>(TClass entity)
    {
        _dbContext.Add(entity!);
    }
}
