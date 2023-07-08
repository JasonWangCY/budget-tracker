namespace BudgetTracker.WebApi.TransferModels;

public class UpdateTransactionTypeRequest
{
    public string TransactionTypeId { get; set; } = null!;
    public string TransactionTypeName { get; set; } = null!;
    public string? Description { get; set; } = null!;
}
