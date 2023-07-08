using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetCategories(IEnumerable<string> categoryIds, string userId);
    Task<List<Category>> ListCategories(string userId);
    Task AddCategories(IEnumerable<Category> categories);
    Task DeleteCategories(IEnumerable<string> categoryIds, string userId);
    Task DeleteDefaultCategories(IEnumerable<string> categoryIds);
}
