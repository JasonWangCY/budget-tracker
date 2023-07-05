using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<List<TransactionType>> GetTransactionTypesIncludingDefaultAsync(string userId);
    Task<List<Transaction>> GetTransactions(IEnumerable<string> transactionIds, string userId);
    Task<List<Transaction>> GetTransactionsWithinDateRangeAsync(DateTime startDate, DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsBeforeDateAsync(DateTime endDate, string userId);
    Task<List<Transaction>> GetTransactionsAfterDateAsync(DateTime startDate, string userId);
    Task<TransactionType?> GetTransactionType(string typeId, string userId);
    Task<List<TransactionType>> GetTransactionTypes(IEnumerable<string> typeIds, string userId);
    Task<List<TransactionType>> GetDefaultTransactionTypes(IEnumerable<string> typeIds);
    Task AddTransactionTypes(IEnumerable<TransactionType> transactionTypes);
    void DeleteTransactionTypes(IEnumerable<TransactionType> transactionTypes);
}
