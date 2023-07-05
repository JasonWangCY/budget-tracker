using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;

namespace BudgetTracker.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TransactionType>> ListTransactionTypes(string userId)
    {
        return await _unitOfWork.Transactions.GetTransactionTypesIncludingDefaultAsync(userId);
    }

    public async Task<List<Transaction>> ListTransactions(
        DateTime? startDate,
        DateTime? endDate,
        string userId)
    {
        var hasStartDate = startDate != null;
        var hasEndDate = endDate != null;

        if (hasStartDate && !hasEndDate)
        {
            var startDateValue = startDate!.Value;
            return await _unitOfWork.Transactions.GetTransactionsAfterDateAsync(startDateValue, userId);
        }
        else if (!hasStartDate && hasEndDate)
        {
            var endDateValue = endDate!.Value;
            return await _unitOfWork.Transactions.GetTransactionsBeforeDateAsync(endDateValue, userId);
        }
        else
        {
            var startDateValue = startDate!.Value;
            var endDateValue = endDate!.Value;
            if (endDateValue < startDateValue)
                throw new ApplicationException("End date cannot be earlier than start date!");

            return await _unitOfWork.Transactions.GetTransactionsWithinDateRangeAsync(startDateValue, endDateValue, userId);
        }
    }

    public async Task<(List<Category>, List<TransactionType>)> GetCategoriesAndTransactionTypes(
        string userId,
        IEnumerable<string> categoryIds,
        IEnumerable<string> transactionTypeIds)
    {
        // Cannot use Task.WhenAll here since dbContext does not support simultaneous calls on one instance.
        var categories = await _unitOfWork.Categories.GetCategories(categoryIds, userId);
        var transactionTypes = await _unitOfWork.Transactions.GetTransactionTypes(transactionTypeIds, userId);

        return (categories, transactionTypes);
    }

    public async Task AddTransactions(IEnumerable<Transaction> transactions)
    {
        await _unitOfWork.Transactions.AddRangeAsync(transactions);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteTransactions(IEnumerable<string> transactionIds, string userId)
    {
        var transactionsToDelete = await _unitOfWork.Transactions.GetTransactions(transactionIds, userId);
        _unitOfWork.Transactions.RemoveRange(transactionsToDelete);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddTransactionTypes(IEnumerable<TransactionType> transactionTypes)
    {
        await _unitOfWork.Transactions.AddTransactionTypes(transactionTypes);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteTransactionTypes(IEnumerable<string> transactionTypeIds, string userId)
    {
        var transactionTypesToDelete = await _unitOfWork.Transactions.GetTransactionTypes(transactionTypeIds, userId);
        _unitOfWork.Transactions.DeleteTransactionTypes(transactionTypesToDelete);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteDefaultTransactionTypes(IEnumerable<string> transactionTypeIds)
    {
        var transactionTypesToDelete = await _unitOfWork.Transactions.GetDefaultTransactionTypes(transactionTypeIds);
        _unitOfWork.Transactions.DeleteTransactionTypes(transactionTypesToDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}
