namespace BudgetTracker.Domain.Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(string categoryName) : base($"Category: '{categoryName}' does not exist.")
    {
    }
}
