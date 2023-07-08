using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetCategoriesIncludingDefaultAsync(string userId);
    Task<Category?> GetCategory(string categoryId, string userId);
    Task<List<Category>> GetCategories(IEnumerable<string> categoryIds, string userId);
    Task<List<Category>> GetDefaultCategories(IEnumerable<string> categoryIds);
    void DeleteRange(IEnumerable<Category> categoriesToRemove);
}
