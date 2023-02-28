using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class CategoryRepository : Repository, ICategoryRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public CategoryRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category?> GetByCategoryAndUserId(string categoryName, string userId)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.CategoryName == categoryName &&
            x.UserId == userId);
    }

    public async Task<Category?> GetById(string categoryId)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
    }
}
