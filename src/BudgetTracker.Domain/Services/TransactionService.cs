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
        DateTime startDate,
        DateTime endDate,
        string userId)
    {
        if (endDate < startDate)
            throw new ApplicationException("End date cannot be earlier than start date!");

        var transactions = await _unitOfWork.TransactionRepo.GetTransactionsWithinTimeRange(startDate, endDate, userId);
        return transactions;
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
        var category = await _unitOfWork.CategoryRepo.GetByCategoryAndUserId(categoryName, userId);
        if (category == null)
            throw new CategoryNotFoundException(categoryName);

        var transactionType = await _unitOfWork.TransactionRepo.GetTypeByUserId(transactionTypeName, userId);
        if (transactionType == null)
            throw new TransactionTypeNotFoundException(transactionTypeName);

        var transactionId = Guid.NewGuid().ToString();
        var transaction = new Transaction(date, transactionId, amount, currency, description, transactionType, category, userId);

        _unitOfWork.TransactionRepo.Add(transaction);
        await _unitOfWork.SaveChangesAsync();
    }
}
