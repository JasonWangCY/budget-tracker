namespace BudgetTracker.Infrastructure.Identity.Interfaces;

public interface IUserService
{
    Task<bool> AddUser(ApplicationUser applicationUser, string password, IEnumerable<string> userRoles);
}
