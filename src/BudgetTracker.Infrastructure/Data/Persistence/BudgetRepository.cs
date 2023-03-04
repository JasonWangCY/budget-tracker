using BudgetTracker.Domain.Entities.BudgetAggregate;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
{
    public BudgetRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
    }
}
