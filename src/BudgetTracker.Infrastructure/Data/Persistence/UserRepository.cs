using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class UserRepository : Repository, IUserRepository
{
    private readonly BudgetTrackerDbContext _dbContext;

    public UserRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User> GetById(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
