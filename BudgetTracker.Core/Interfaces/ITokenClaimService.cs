using Microsoft.IdentityModel.Tokens;

namespace BudgetTracker.Core.Interfaces;

public interface ITokenClaimService
{
    SecurityToken GetToken(string userName, IList<string> userRoles, string issuer, string audience);
}
