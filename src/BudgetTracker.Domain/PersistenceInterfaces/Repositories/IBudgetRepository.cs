using BudgetTracker.Domain.Entities.BudgetAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface IBudgetRepository : IGenericRepository<Budget>
{
}
