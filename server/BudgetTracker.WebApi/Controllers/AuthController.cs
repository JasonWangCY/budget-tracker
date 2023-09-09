using BudgetTracker.Application.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.Configs.Models;
using BudgetTracker.WebApi.TransferModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenClaimService _tokenClaimService;
    private readonly JwtOptions _jwtOptions;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        ITokenClaimService tokenClaimService,
        JwtOptions jwtOptions)
    {
        _userManager = userManager;
        _tokenClaimService = tokenClaimService;
        _jwtOptions = jwtOptions;
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

        var issuer = _jwtOptions.ValidIssuer;
        var audience = _jwtOptions.ValidAudience;
        var token = _tokenClaimService.GetToken(user.UserName, user.Id, userRoles, issuer, audience);

        return Ok(new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });
    }
}