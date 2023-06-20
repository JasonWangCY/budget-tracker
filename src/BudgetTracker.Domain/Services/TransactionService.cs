using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.Exceptions;
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
        var getCategoryTask = _unitOfWork.Categories.GetCategories(categoryIds, userId);
        var getTransactionTypeTask = _unitOfWork.Transactions.GetTransactionTypes(transactionTypeIds, userId);
        await Task.WhenAll(getCategoryTask, getTransactionTypeTask);

        var categories = getCategoryTask.Result;
        var transactionTypes = getTransactionTypeTask.Result;
        return (categories, transactionTypes);
    }

    public async Task AddTransactions(List<Transaction> transactions)
    {
        // TODO: Handle possible exceptions?
        await _unitOfWork.Transactions.AddRangeAsync(transactions);
        await _unitOfWork.SaveChangesAsync();
    }
}
