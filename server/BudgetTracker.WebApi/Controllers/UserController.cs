using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.Infrastructure.Identity.Interfaces;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Application.Constants.Constants;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserService _userService;

    public UserController(
        UserManager<ApplicationUser> userManager,
        IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [HttpGet]
    [Route("checkAdmin")]
    public bool CheckAdmin()
    {
        return User.IsInRole(UserRole.ADMIN);
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RegisterResponse))]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.UserName);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = "User already exists!"
            });
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var userRoles = new List<string> { UserRole.USER };
        var saveUserSucceeded = await _userService.AddUser(user, request.Password, userRoles);
        if (!saveUserSucceeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = "User creation failed! Please check user details and try again."
            });
        }

        return Ok(new RegisterResponse
        {
            Status = RegisterStatus.SUCCESS,
            Message = "User created successfully!"
        });
    }

    [HttpPost]
    [Route("registerAdmin")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RegisterResponse))]
    public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.UserName);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = "User already exists!"
            });
        }

        var adminUser = new ApplicationUser
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var adminUserRoles = new List<string> { UserRole.ADMIN, UserRole.USER };
        var saveUserSucceeded = await _userService.AddUser(adminUser, request.Password, adminUserRoles);
        if (!saveUserSucceeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = "User creation failed! Please check user details and try again."
            });
        }

        return Ok(new RegisterResponse
        {
            Status = RegisterStatus.SUCCESS,
            Message = "User created successfully!"
        });
    }
}
