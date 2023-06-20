namespace BudgetTracker.WebApi.TransferModels;

public class CategoryDto
{
    public string CategoryId { get; init; } = null!;
    public string CategoryName { get; init; } = null!;
    public string? Description { get; init; }
    public bool IsDefaultCategory { get; init; } = false;
}
