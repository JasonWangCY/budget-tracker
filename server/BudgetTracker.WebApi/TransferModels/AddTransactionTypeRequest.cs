namespace BudgetTracker.WebApi.TransferModels;

public class AddTransactionTypeRequest
{
    public string TransactionTypeName { get; set; } = null!;
    public string? Description { get; set; } = null!;
}
