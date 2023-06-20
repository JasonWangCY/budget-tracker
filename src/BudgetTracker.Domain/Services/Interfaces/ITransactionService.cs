using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.Services.Interfaces;

public interface ITransactionService
{
    Task<List<Transaction>> ListTransactions(DateTime? startDate, DateTime? endDate, string userId);
    Task<(List<Category>, List<TransactionType>)> GetCategoriesAndTransactionTypes(
        string userId,
        IEnumerable<string> categoryIds,
        IEnumerable<string> transactionTypeIds);
    Task AddTransactions(List<Transaction> transactions);
}
