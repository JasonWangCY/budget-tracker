using Microsoft.IdentityModel.Tokens;

namespace BudgetTracker.Application.Services.Interfaces;

public interface ITokenClaimService
{
    SecurityToken GetToken(string userName, string userId, IList<string> userRoles, string issuer, string audience);
}
