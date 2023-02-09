using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.TransferModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Common.Constants;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.UserName);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = "Error",
                Message = "User already exists!"
            });
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        // TODO: How to propagate this to client side? Since they need to know if the password is weak.
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              new RegisterResponse
                              {
                                  Status = "Error",
                                  Message = "User creation failed! Please check user details and try again."
                              });
        }

        return Ok(new RegisterResponse
        {
            Status = "Success",
            Message = "User created successfully!"
        });
    }

    [Authorize]
    [HttpPost]
    [Route("registerAdmin")]
    public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.UserName);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = "Error",
                Message = "User already exists!"
            });
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              new RegisterResponse
                              {
                                  Status = "Error",
                                  Message = "User creation failed! Please check user details and try again."
                              });
        }

        if (await _roleManager.RoleExistsAsync(UserRole.ADMIN))
            await _userManager.AddToRoleAsync(user, UserRole.ADMIN);
        if (await _roleManager.RoleExistsAsync(UserRole.ADMIN))
            await _userManager.AddToRoleAsync(user, UserRole.USER);

        return Ok(new RegisterResponse
        {
            Status = "Success",
            Message = "User created successfully!"
        });
    }
}
