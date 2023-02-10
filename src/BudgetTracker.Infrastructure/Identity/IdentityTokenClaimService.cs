using BudgetTracker.Core.Constants;
using BudgetTracker.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace BudgetTracker.Infrastructure.Identity;

public class IdentityTokenClaimService : ITokenClaimService
{
    public SecurityToken GetToken(string userName, IList<string> userRoles, string issuer, string audience)
    {
        var secretKey = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
        var authSigningKey = new SymmetricSecurityKey(secretKey);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.Now.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
