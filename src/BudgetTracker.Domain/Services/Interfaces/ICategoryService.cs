using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> ListCategories(string userId);
}
