using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<List<Transaction>> GetTransactionsWithinDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsBeforeDateAsync(DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsAfterDateAsync(DateTime startDate, string userId);
    Task<TransactionType?> GetTransactionType(string typeId, string userId);
    Task<List<TransactionType>> GetTransactionTypes(IEnumerable<string> typeIds, string userId);
}
