using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.TransferModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Application.Constants;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName
        };
        var result = await _userManager.CreateAsync(user, request.Password);

        // TODO: How to propagate this to client side? Since they need to know if the password is weak.
        if (!result.Succeeded)
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
    [Authorize(Roles = UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RegisterResponse))]
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
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName
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

        return Ok(new RegisterResponse
        {
            Status = RegisterStatus.SUCCESS,
            Message = "User created successfully!"
        });
    }
}
