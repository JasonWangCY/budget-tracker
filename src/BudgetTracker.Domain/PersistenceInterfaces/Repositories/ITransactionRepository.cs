using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ITransactionRepository : IRepository
{
    Task<TransactionType> GetTypeByUserId(string typeName, string userId);
    Task<List<Transaction>> GetTransactionsWithinTimeRange(DateTime startDate, DateTime endDate, string userId);
}
