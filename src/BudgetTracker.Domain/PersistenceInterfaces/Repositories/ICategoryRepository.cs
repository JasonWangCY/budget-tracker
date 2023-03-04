using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetByCategoryAndUserId(string categoryId, string userId);
}
