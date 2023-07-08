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

    public async Task<List<Category>> GetCategories(IEnumerable<string> categoryIds, string userId)
    {
        return await _unitOfWork.Categories.GetCategories(categoryIds, userId);
    }

    public async Task<List<Category>> ListCategories(string userId)
    {
        return await _unitOfWork.Categories.GetCategoriesIncludingDefaultAsync(userId);
    }

    public async Task AddCategories(IEnumerable<Category> categories)
    {
        await _unitOfWork.Categories.AddRangeAsync(categories);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCategories(IEnumerable<string> categoryIds, string userId)
    {
        var categoriesToDelete = await GetCategories(categoryIds, userId);
        _unitOfWork.Categories.DeleteRange(categoriesToDelete);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteDefaultCategories(IEnumerable<string> categoryIds)
    {
        var categoriesToDelete = await _unitOfWork.Categories.GetDefaultCategories(categoryIds);
        _unitOfWork.Categories.DeleteRange(categoriesToDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}
