namespace BudgetTracker.WebApi.TransferModels;

public class AddCategoryRequest
{
    public string CategoryName { get; init; } = null!;
    public string? Description { get; init; }
}
