using BudgetTracker.Domain.Entities;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Services.Interfaces;
using BudgetTracker.Domain.Entities.TransactionAggregate;

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

    public IEnumerable<CategoryDto> ConvertToCategoryDto(IEnumerable<Category> categories)
    {
        return categories.Select(x => new CategoryDto
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Description = x.Description,
            IsDefaultCategory = x.IsDefaultCategory,
        });
    }

    public IEnumerable<TransactionDto> ConvertToTransactionDto(IEnumerable<Transaction> transactions)
    {
        return transactions.Select(x => new TransactionDto
        {
            TransactionId = x.TransactionId,
            TransactionDate = x.TransactionDate,
            TransactionAmount = x.TransactionAmount,
            Currency = x.Currency,
            TransactionTypeName = x.TransactionType.TransactionTypeName,
            CategoryName = x.Category.CategoryName
        });
    }

    public IEnumerable<TransactionType> ConvertToTransactionTypeDomain(
        IEnumerable<AddTransactionTypeRequest> requests,
        bool isDefaultType,
        string? userId=null)
    {
        return requests.Select(x => new TransactionType(
            x.TransactionTypeName,
            x.Description,
            x.Sign,
            isDefaultType,
            userId));
    }

    public IEnumerable<TransactionTypeDto> ConvertToTransactionTypeDto(
        IEnumerable<TransactionType> transactionTypes)
    {
        return transactionTypes.Select(x => new TransactionTypeDto
        {
            TransactionTypeId = x.TransactionTypeId,
            TransactionTypeName = x.TransactionTypeName,
            Description = x.Description,
            Sign = x.Sign,
            IsDefaultType = x.IsDefaultType,
        });
    }
}