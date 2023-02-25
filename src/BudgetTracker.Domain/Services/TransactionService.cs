using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ILogger<TransactionService> logger)
    {
        _transactionRepository = transactionRepository;
        _logger = logger;
    }

    public async Task<Transaction> AddTransaction()
    {
        var 
    }
}
