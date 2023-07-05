using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.Services.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionType>> ListTransactionTypes(string userId);
    Task<List<Transaction>> ListTransactions(DateTime? startDate, DateTime? endDate, string userId);
    Task<(List<Category>, List<TransactionType>)> GetCategoriesAndTransactionTypes(
        string userId,
        IEnumerable<string> categoryIds,
        IEnumerable<string> transactionTypeIds);
    Task AddTransactions(IEnumerable<Transaction> transactions);
    Task DeleteTransactions(IEnumerable<string> transactionIds, string userId);
    Task AddTransactionTypes(IEnumerable<TransactionType> transactionTypes);
    Task DeleteTransactionTypes(IEnumerable<string> transactionTypeIds, string userId);
    Task DeleteDefaultTransactionTypes(IEnumerable<string> transactionTypeIds);
}
