using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.Domain.Services.Interfaces;

public interface ITransactionService
{
    Task<List<Transaction>> ListTransactions(DateTime startDate, DateTime endDate, string userId);
    Task AddTransaction(DateTime date, decimal amount, string? currency, string? description, string transactionTypeName,
                        string categoryName, string userId);
}
