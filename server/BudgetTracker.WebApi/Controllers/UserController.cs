using BudgetTracker.Application.Utils;
using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RegisterResponse))]
    // TODO: Potential issue: what if system crashes between saving in Identity DB and saving in Domain DB?
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
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = result.Errors.Select(x => x.Code).DumpJson()
            });
        }

        await _userService.AddUser(user.Id, user.UserName, user.FirstName, user.LastName);
        return Ok(new RegisterResponse
        {
            Status = RegisterStatus.SUCCESS,
            Message = "User created successfully!"
        });
    }

    [HttpPost]
    [Route("registerAdmin")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RegisterResponse))]
    // TODO: Potential issue: what if system crashes between saving in Identity DB and saving in Domain DB?
    public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
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
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse
            {
                Status = RegisterStatus.ERROR,
                Message = "User creation failed! Please check user details and try again."
            });
        }

        await _userService.AddUser(user.Id, user.UserName, user.FirstName, user.LastName);
        return Ok(new RegisterResponse
        {
            Status = RegisterStatus.SUCCESS,
            Message = "User created successfully!"
        });
    }
}
