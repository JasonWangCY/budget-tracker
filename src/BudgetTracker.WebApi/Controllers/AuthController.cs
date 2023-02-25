using BudgetTracker.Application.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.TransferModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BudgetTracker.WebApi.Controllers;

// TODO: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-6.0
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenClaimService _tokenClaimService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ITokenClaimService tokenClaimService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenClaimService = tokenClaimService;
    }

    [HttpPost]
    [Route("auth")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Auth(AuthRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var issuer = _configuration["JWT:ValidIssuer"];
        var audience = _configuration["JWT:ValidAudience"];
        var token = _tokenClaimService.GetToken(user.UserName, userRoles, issuer, audience);

        return Ok(new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });
    }
}