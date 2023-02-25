using BudgetTracker.Domain.Entities.BudgetAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface IBudgetRepository : IRepository<Budget>
{
}
