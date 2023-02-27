namespace BudgetTracker.WebApi.TransferModels;

public class ListTransactionsResponse
{
    public string TransactionId { get; set; } = null!;
    public DateTime TransactionDate { get; set; }
    public decimal TransactionAmount { get; set; }
    public string? Currency { get; set; }
    public string TransactionTypeName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}

public class AddTransactionRequest
{
    public DateTime TransactionDate { get; set; }
    public decimal TransactionAmount { get; set; }
    public string? Currency { get; set; }
    public string? Description { get; set; } = null!;
    public string TransactionTypeName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}