using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Data.Persistence;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
