namespace BudgetTracker.WebApi.TransferModels;

public class AddCategoryRequest
{
    public string CategoryId { get; init; } = null!;
    public string CategoryName { get; init; } = null!;
    public string? Description { get; init; }
}
