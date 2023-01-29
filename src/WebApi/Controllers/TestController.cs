using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpPost("TestPost")]
    public IEnumerable<string> Get(IEnumerable<string> list)
    {
        return list;
    }
}