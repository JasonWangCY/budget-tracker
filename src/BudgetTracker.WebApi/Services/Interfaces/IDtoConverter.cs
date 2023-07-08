using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.WebApi.TransferModels;

namespace BudgetTracker.WebApi.Services.Interfaces;

public interface IDtoConverter
{
    IEnumerable<Category> ConvertToCategoryDomain(
        IEnumerable<AddCategoryRequest> requests,
        bool isDefaultCategory,
        string? userId=null);
    IEnumerable<CategoryDto> ConvertToCategoryDto(IEnumerable<Category> categories);

    IEnumerable<TransactionDto> ConvertToTransactionDto(IEnumerable<Transaction> transactions);
    IEnumerable<TransactionType> ConvertToTransactionTypeDomain(
        IEnumerable<AddTransactionTypeRequest> requests,
        bool isDefaultType,
        string? userId = null);
    IEnumerable<TransactionTypeDto> ConvertToTransactionTypeDto(
        IEnumerable<TransactionType> transactionTypes);
    void UpdateCategoriesDomain(
        IEnumerable<UpdateCategoryRequest> categoryDtos,
        IEnumerable<Category> categories);
    void UpdateTransactionsDomain(
        IEnumerable<UpdateTransactionRequest> transactionDtos,
        IEnumerable<Transaction> transactions);
    void UpdateTransactionTypesDomain(
        IEnumerable<UpdateTransactionTypeRequest> transactionTypeDtos,
        IEnumerable<TransactionType> transactionTypes);
}
