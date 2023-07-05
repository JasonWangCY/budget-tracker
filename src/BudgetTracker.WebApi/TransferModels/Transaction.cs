namespace BudgetTracker.WebApi.TransferModels;

public class TransactionDto
{
    public string TransactionId { get; init; } = null!;
    public DateTime TransactionDate { get; init; }
    public decimal TransactionAmount { get; init; }
    public string? Currency { get; init; }
    public string TransactionTypeName { get; init; } = null!;
    public string CategoryName { get; init; } = null!;
}

public class AddTransactionRequest
{
    public DateTime TransactionDate { get; init; }
    public decimal TransactionAmount { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; } = null!;
    public string TransactionTypeId { get; init; } = null!;
    public string CategoryId { get; init; } = null!;
}