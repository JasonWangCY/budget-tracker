using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface ICategoryRepository : IRepository
{
    Task<Category> GetById(string categoryId);
    Task<Category> GetByCategoryAndUserId(string categoryId, string userId);
}
