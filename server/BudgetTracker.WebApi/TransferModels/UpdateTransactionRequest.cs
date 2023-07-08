namespace BudgetTracker.WebApi.TransferModels;

public class UpdateTransactionRequest
{
    public string TransactionId { get; set; } = null!;
    public DateTime TransactionDate { get; init; }
    public decimal TransactionAmount { get; init; }
    public string Currency { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public string TransactionTypeId { get; init; } = null!;
    public string CategoryId { get; init; } = null!;
}
