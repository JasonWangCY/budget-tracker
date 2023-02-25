using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces;

public interface ITransactionRepository : IRepository<Transaction>
{

}
