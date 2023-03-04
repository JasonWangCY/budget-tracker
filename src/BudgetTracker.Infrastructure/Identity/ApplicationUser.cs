using Microsoft.AspNetCore.Identity;

namespace BudgetTracker.Infrastructure.Identity;

// TODO: Add some properties?
// https://stackoverflow.com/a/31340030/13956054
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
}