using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<TransactionType?> GetTypeByUserId(string typeName, string userId);
    Task<List<Transaction>> GetTransactionsWithinDateRange(DateTime startDate, DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsBeforeDate(DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsAfterDate(DateTime startDate, string userId);
}
