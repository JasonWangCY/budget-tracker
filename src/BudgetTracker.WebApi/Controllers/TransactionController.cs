using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.Exceptions;
using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BudgetTracker.Application.Constants.Constants;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
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
    [Route("listTransactions")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionDto>))]
    public async Task<IActionResult> ListTransactions(DateTime? startDate=null, DateTime? endDate=null)
    {
        var userId = _userManager.GetUserId(User);
        var transactions = await _transactionService.ListTransactions(startDate, endDate, userId);
        var transactionDtos = transactions.Select(x => new TransactionDto
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
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddTransactions(List<AddTransactionRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        var categoryIds = requests.DistinctBy(x => x.CategoryId).Select(x => x.CategoryId);
        var transactionTypeIds = requests.DistinctBy(x => x.TransactionTypeId).Select(x => x.TransactionTypeId);
        var (categories, transactionTypes) = await _transactionService.GetCategoriesAndTransactionTypes(userId, transactionTypeIds, transactionTypeIds);
        var categoriesSet = categories.ToHashSet();
        var transactionTypesSet = transactionTypes.ToHashSet();

        var transactions = new List<Transaction>();
        foreach (var request in requests)
        {
            var category = categoriesSet.FirstOrDefault(x => x.CategoryId == request.CategoryId);
            var transactionType = transactionTypesSet.FirstOrDefault(x => x.TransactionTypeId == request.TransactionTypeId);

            // TODO: We should not throw error here... return error response instead.
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryName);
            }
            if (transactionType == null)
            {
                throw new TransactionTypeNotFoundException(request.TransactionTypeName);
            }

            var transactionId = Guid.NewGuid().ToString();
            transactions.Add(new Transaction(request.TransactionDate, transactionId, request.TransactionAmount, 
                request.Currency, request.Description, transactionType, category, userId));
        }

        await _transactionService.AddTransactions(transactions);

        return Ok(new GenericResponse { HasError = false });
    }
}
