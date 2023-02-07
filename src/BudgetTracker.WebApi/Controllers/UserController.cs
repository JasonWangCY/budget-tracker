using BudgetTracker.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationContext _dbContext;

    public UserController(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("test")]
    public IActionResult Test()
    {
        return Ok("Successful!");
    }
}
