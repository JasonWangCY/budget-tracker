using BudgetTracker.Domain.Entities.BudgetAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class BudgetRepository : Repository, IBudgetRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public BudgetRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
