namespace BudgetTracker.Domain.Services.Interfaces;

public interface ITransactionService
{
    Task AddTransaction(DateTime date, decimal amount, string? currency, string? description, string transactionTypeName,
                        string categoryName, string userId);
}
