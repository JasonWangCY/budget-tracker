using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.TransferModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Application.Constants.Constants;

namespace BudgetTracker.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TransactionController(
        ITransactionService transactionService,
        UserManager<ApplicationUser> userManager)
    {
        _transactionService = transactionService;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("list")]
    [Authorize(Roles = UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListTransactionsResponse>))]
    public async Task<IActionResult> ListTransactions(DateTime? startDate=null, DateTime? endDate=null)
    {
        var userId = _userManager.GetUserId(User);
        var transactions = await _transactionService.ListTransactions(startDate, endDate, userId);
        var transactionDtos = transactions.Select(x => new ListTransactionsResponse
        {
            TransactionId = x.TransactionId,
            TransactionDate = x.TransactionDate,
            TransactionAmount = x.TransactionAmount,
            Currency = x.Currency,
            TransactionTypeName = x.TransactionType.TransactionTypeName,
            CategoryName = x.Category.CategoryName
        });

        return Ok(transactionDtos);
    }

    [HttpPost]
    [Route("addTransaction")]
    [Authorize(Roles = UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddTransaction(AddTransactionRequest request)
    {
        var userId = _userManager.GetUserId(User);
        // TODO: Handle exception
        await _transactionService.AddTransaction(request.TransactionDate, request.TransactionAmount, request.Currency,
            request.Description, request.TransactionTypeName, request.CategoryName, userId);

        return Ok(new GenericResponse { HasError = false });
    }
}
