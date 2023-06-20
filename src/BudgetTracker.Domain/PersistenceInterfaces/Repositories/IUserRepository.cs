using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
}
