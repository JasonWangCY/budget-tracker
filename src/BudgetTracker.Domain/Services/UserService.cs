using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Domain.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUnitOfWork unitOfWork,
        ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task AddUser(string userId, string userName, string firstName, string? lastName)
    {
        var user = new User(userId, userName, firstName, lastName);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
