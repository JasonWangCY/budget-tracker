namespace BudgetTracker.WebApi.TransferModels;

public class UpdateCategoryRequest
{
    public string CategoryId { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
}
