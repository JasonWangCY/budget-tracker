using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BudgetTracker.WebApi.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<bool> AddUser(ApplicationUser applicationUser, string password, IEnumerable<string> userRoles)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            var user = new User(applicationUser.Id, applicationUser.UserName, applicationUser.FirstName, applicationUser.LastName);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var addUserResult = await _userManager.CreateAsync(applicationUser, password);
            if (!addUserResult.Succeeded)
            {
                _logger.LogError("Failed to create username {username}. Reason: {reason}",
                    applicationUser.UserName, 
                    string.Join(", ", addUserResult.Errors.Select(x => x.Description)));
                return false;
            }

            foreach (var userRole in userRoles)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(applicationUser, userRole);
                if (!addRoleResult.Succeeded)
                {
                    _logger.LogError("Failed to add user role: {role}. Reason: {reason}", 
                        userRole, string.Join(", ", addRoleResult.Errors.Select(x => x.Description)));
                    return false;
                }
            }

            transaction.Commit();
        }

        return true;
    }
}
