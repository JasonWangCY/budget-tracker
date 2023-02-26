using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<TransactionType> GetTypeByUserId(string typeName, string userId);
}
