using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class CategoryRepository : Repository, ICategoryRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public CategoryRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Category> GetByCategoryAndUserId(string categoryId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetById(string categoryId)
    {
        throw new NotImplementedException();
    }
}
