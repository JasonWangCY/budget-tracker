namespace BudgetTracker.WebApi.TransferModels;

public class TransactionDto
{
    public string TransactionId { get; init; } = null!;
    public DateTime TransactionDate { get; init; }
    public decimal TransactionAmount { get; init; }
    public string? Description { get; set; }
    public string Currency { get; init; } = null!;
    public string TransactionTypeId { get; set; } = null!;
    public string TransactionTypeName { get; init; } = null!;
    public string CategoryId { get; set; } = null!;
    public string CategoryName { get; init; } = null!;
}

public class AddTransactionRequest
{
    public DateTime TransactionDate { get; init; }
    public decimal TransactionAmount { get; init; }
    public string Currency { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string TransactionTypeId { get; init; } = null!;
    public string CategoryId { get; init; } = null!;
}