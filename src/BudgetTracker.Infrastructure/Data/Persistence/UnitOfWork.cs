using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BudgetTrackerDbContext _dbContext;
    private readonly IUserRepository _userRepo;
    private readonly IBudgetRepository _budgetRepo;
    private readonly ITransactionRepository _transactionRepo;
    private readonly ICategoryRepository _categoryRepo;

    public UnitOfWork(
        BudgetTrackerDbContext dbContext,
        IUserRepository userRepo,
        IBudgetRepository budgetRepo,
        ITransactionRepository transactionRepo,
        ICategoryRepository categoryRepo)
    {
        _dbContext = dbContext;
        _userRepo = userRepo;
        _budgetRepo = budgetRepo;
        _transactionRepo = transactionRepo;
        _categoryRepo = categoryRepo;
    }

    public IUserRepository UserRepo => _userRepo;
    public IBudgetRepository BudgetRepo => _budgetRepo;
    public ITransactionRepository TransactionRepo => _transactionRepo;
    public ICategoryRepository CategoryRepo => _categoryRepo;

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
