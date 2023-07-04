using BudgetTracker.Domain.Entities;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Services.Interfaces;

namespace BudgetTracker.WebApi.Services;

public class DtoConverter : IDtoConverter
{
    public IEnumerable<Category> ConvertToCategoryDomain(
        IEnumerable<AddCategoryRequest> requests,
        bool isDefaultCategory,
        string? userId=null)
    {
        return requests.Select(x => new Category(
            x.CategoryName,
            x.Description,
            isDefaultCategory,
            userId));
    }
}