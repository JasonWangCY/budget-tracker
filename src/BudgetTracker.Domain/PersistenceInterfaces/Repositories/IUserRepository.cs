using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.PersistenceInterfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByUserName(string userName);
    Task<User> GetById(string userId);
}
