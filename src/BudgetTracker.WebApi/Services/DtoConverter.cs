using BudgetTracker.Domain.Entities;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Services.Interfaces;
using BudgetTracker.Domain.Entities.TransactionAggregate;

namespace BudgetTracker.WebApi.Services;

public class DtoConverter : IDtoConverter
{
    private readonly ILogger<DtoConverter> _logger;

    public DtoConverter(ILogger<DtoConverter> logger)
    {
        _logger = logger;
    }

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
            Description = x.Description,
            Currency = x.Currency,
            TransactionTypeId = x.TransactionTypeId,
            TransactionTypeName = x.TransactionType.TransactionTypeName,
            CategoryId = x.CategoryId,
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
            IsDefaultType = x.IsDefaultType,
        });
    }

    public void UpdateCategoriesDomain(
        IEnumerable<UpdateCategoryRequest> categoryDtos, 
        IEnumerable<Category> categories)
    {
        foreach (var categoryDto in categoryDtos)
        {
            var categoryId = categoryDto.CategoryId;
            var category = categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category == null)
            {
                _logger.LogWarning("Cannot find category: {Id}", categoryId);
                continue;
            }

            category.UpdateCategory(categoryDto.CategoryName, categoryDto.Description);
        }
    }

    public void UpdateTransactionsDomain(
        IEnumerable<UpdateTransactionRequest> transactionDtos,
        IEnumerable<Transaction> transactions)
    {
        foreach (var transactionDto in transactionDtos)
        {
            var transactionId = transactionDto.TransactionId;
            var transaction = transactions.FirstOrDefault(x => x.TransactionId == transactionId);
            if (transaction == null)
            {
                _logger.LogWarning("Cannot find transaction: {Id}", transactionId);
                continue;
            }

            transaction.UpdateTransaction(
                transactionDto.TransactionDate,
                transactionDto.TransactionAmount,
                transactionDto.Currency,
                transactionDto.Description,
                transactionDto.TransactionTypeId,
                transactionDto.CategoryId);
        }
    }

    public void UpdateTransactionTypesDomain(
        IEnumerable<UpdateTransactionTypeRequest> transactionTypeDtos,
        IEnumerable<TransactionType> transactionTypes)
    {
        foreach (var transactionTypeDto in transactionTypeDtos)
        {
            var transactionTypeId = transactionTypeDto.TransactionTypeId;
            var transactionType = transactionTypes.FirstOrDefault(x => x.TransactionTypeId == transactionTypeId);
            if (transactionType == null)
            {
                _logger.LogWarning("Cannot find transaction type: {Id}", transactionTypeId);
                continue;
            }

            transactionType.UpdateTransactionType(
                transactionTypeDto.TransactionTypeName,
                transactionTypeDto.Description);
        }
    }
}