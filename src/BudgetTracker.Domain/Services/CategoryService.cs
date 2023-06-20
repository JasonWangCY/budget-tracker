using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;

namespace BudgetTracker.Domain.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Category>> ListCategories(string userId)
    {
        return await _unitOfWork.Categories.GetCategoriesIncludingDefaultAsync(userId);
    }

    public async Task AddCategories()
    {

    }
}
