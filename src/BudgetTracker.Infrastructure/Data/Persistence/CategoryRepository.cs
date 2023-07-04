using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Category>> GetCategoriesIncludingDefaultAsync(string userId)
    {
        return await dbSet.Where(x => x.UserId == userId || x.IsDefaultCategory).ToListAsync();
    }

    public async Task<Category?> GetCategory(string categoryId, string userId)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.UserId == userId);
    }

    public async Task<List<Category>> GetCategories(IEnumerable<string> categoryIds, string userId)
    {
        return await dbSet.Where(x => x.UserId == userId && categoryIds.Contains(x.CategoryId)).ToListAsync();
    }

    public async Task<List<Category>> GetDefaultCategories(IEnumerable<string> categoryIds)
    {
        return await dbSet.Where(x => x.IsDefaultCategory && categoryIds.Contains(x.CategoryId)).ToListAsync();
    }

    public void DeleteRange(IEnumerable<Category> categoriesToRemove)
    {
        dbSet.RemoveRange(categoriesToRemove);
    }
}
