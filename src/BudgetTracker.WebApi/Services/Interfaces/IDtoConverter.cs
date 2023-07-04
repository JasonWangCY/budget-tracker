using BudgetTracker.Domain.Entities;
using BudgetTracker.WebApi.TransferModels;

namespace BudgetTracker.WebApi.Services.Interfaces;

public interface IDtoConverter
{
    IEnumerable<Category> ConvertToCategoryDomain(
        IEnumerable<AddCategoryRequest> requests,
        bool isDefaultCategory,
        string? userId=null);
}
