namespace BudgetTracker.Domain.Services.Interfaces;

public interface IUserService
{
    Task AddUser(string userId, string userName, string firstName, string? lastName);
}
