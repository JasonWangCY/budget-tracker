using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.Exceptions;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        IUnitOfWork unitOfWork,
        ILogger<TransactionService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
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
            return await _unitOfWork.Transactions.GetTransactionsAfterDate(startDateValue, userId);
        }
        else if (!hasStartDate && hasEndDate)
        {
            var endDateValue = endDate!.Value;
            return await _unitOfWork.Transactions.GetTransactionsBeforeDate(endDateValue, userId);
        }
        else
        {
            var startDateValue = startDate!.Value;
            var endDateValue = endDate!.Value;
            if (endDateValue < startDateValue)
                throw new ApplicationException("End date cannot be earlier than start date!");

            return await _unitOfWork.Transactions.GetTransactionsWithinDateRange(startDateValue, endDateValue, userId);
        }
    }

    public async Task AddTransaction(
        DateTime date,
        decimal amount,
        string? currency,
        string? description,
        string transactionTypeName,
        string categoryName,
        string userId
        )
    {
        var category = await _unitOfWork.Categories.GetByCategoryAndUserId(categoryName, userId);
        if (category == null)
            throw new CategoryNotFoundException(categoryName);

        var transactionType = await _unitOfWork.Transactions.GetTypeByUserId(transactionTypeName, userId);
        if (transactionType == null)
            throw new TransactionTypeNotFoundException(transactionTypeName);

        var transactionId = Guid.NewGuid().ToString();
        var transaction = new Transaction(date, transactionId, amount, currency, description, transactionType, category, userId);

        _unitOfWork.Transactions.Add(transaction);
        await _unitOfWork.SaveChangesAsync();
    }
}
