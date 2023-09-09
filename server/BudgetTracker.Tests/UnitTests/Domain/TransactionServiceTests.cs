using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services;
using Moq;

namespace BudgetTracker.Tests.UnitTests.Domain;

public class TransactionServiceTests
{
    private readonly string _userId = "userA";
    private const string Currency = default;
    private const TransactionType TransactionType = default;
    private const Category Category = default;
    
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public TransactionServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public void ListTransactions_NoEndDate_NonNullResults()
    {
        var startDate = new DateTime(2023, 01, 01);
        DateTime? endDate = null;

        var transactionId = "transactionA";
        var inputTransactions = new List<Transaction>
        {
            new Transaction(default, transactionId, default, Currency, default, TransactionType, Category, _userId)
        };
        _unitOfWorkMock.Setup(x =>
            x.Transactions.GetTransactionsAfterDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()))
            .ReturnsAsync(inputTransactions);

        var transactionService = new TransactionService(_unitOfWorkMock.Object);
        var transactions = transactionService.ListTransactions(startDate, endDate, transactionId).GetAwaiter().GetResult();

        Assert.Single(transactions);
        Assert.Equal(transactionId, transactions.First().TransactionId);
        _unitOfWorkMock.Verify(x => 
            x.Transactions.GetTransactionsBeforeDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsWithinDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ListTransactions_NoStartDate_NonNullResults()
    {
        DateTime? startDate = null;
        var endDate = new DateTime(2023, 01, 01);

        var transactionId = "transactionA";
        var inputTransactions = new List<Transaction>
        {
            new Transaction(default, transactionId, default, Currency, default, TransactionType, Category, _userId)
        };
        _unitOfWorkMock.Setup(x =>
            x.Transactions.GetTransactionsBeforeDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()))
            .ReturnsAsync(inputTransactions);

        var transactionService = new TransactionService(_unitOfWorkMock.Object);
        var transactions = transactionService.ListTransactions(startDate, endDate, transactionId).GetAwaiter().GetResult();

        Assert.Single(transactions);
        Assert.Equal(transactionId, transactions.First().TransactionId);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsAfterDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsWithinDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ListTransactions_SameStartAndEndDates_NonNullResults()
    {
        var startDate = new DateTime(2023, 01, 01);
        var endDate = new DateTime(2023, 01, 02);

        var transactionId = "transactionA";
        var inputTransactions = new List<Transaction>
        {
            new Transaction(default, transactionId, default, Currency, default, TransactionType, Category, _userId)
        };
        _unitOfWorkMock.Setup(x =>
            x.Transactions.GetTransactionsWithinDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .ReturnsAsync(inputTransactions);

        var transactionService = new TransactionService(_unitOfWorkMock.Object);
        var transactions = transactionService.ListTransactions(startDate, endDate, transactionId).GetAwaiter().GetResult();

        Assert.Single(transactions);
        Assert.Equal(transactionId, transactions.First().TransactionId);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsBeforeDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsAfterDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ListTransactions_HasBothDates_NonNullResults()
    {
        var startDate = new DateTime(2023, 01, 01);
        var endDate = startDate;

        var transactionId = "transactionA";
        var inputTransactions = new List<Transaction>
        {
            new Transaction(default, transactionId, default, Currency, default, TransactionType, Category, _userId)
        };
        _unitOfWorkMock.Setup(x =>
            x.Transactions.GetTransactionsWithinDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
            .ReturnsAsync(inputTransactions);

        var transactionService = new TransactionService(_unitOfWorkMock.Object);
        var transactions = transactionService.ListTransactions(startDate, endDate, transactionId).GetAwaiter().GetResult();

        Assert.Single(transactions);
        Assert.Equal(transactionId, transactions.First().TransactionId);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsBeforeDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
        _unitOfWorkMock.Verify(x =>
            x.Transactions.GetTransactionsAfterDateAsync(It.IsAny<DateTime>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void ListTransactions_EndDateBeforeStartDate_ThrowException()
    {
        var startDate = new DateTime(2023, 01, 02);
        var endDate = new DateTime(2023, 01, 01);

        var transactionId = "transactionA";
        var transactionService = new TransactionService(_unitOfWorkMock.Object);

        Assert.Throws<ApplicationException>(() =>
        {
            transactionService.ListTransactions(startDate, endDate, transactionId).GetAwaiter().GetResult();
        });
    }
}
