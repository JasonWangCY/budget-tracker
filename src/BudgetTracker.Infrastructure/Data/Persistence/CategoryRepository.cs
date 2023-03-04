using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Category?> GetByCategoryAndUserId(string categoryName, string userId)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.CategoryName == categoryName && x.UserId == userId);
    }
}
