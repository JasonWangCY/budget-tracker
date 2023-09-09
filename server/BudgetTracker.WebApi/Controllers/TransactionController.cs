using BudgetTracker.Application.Utils;
using BudgetTracker.Domain.Entities.TransactionAggregate;
using BudgetTracker.Domain.Exceptions;
using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.Services.Interfaces;
using BudgetTracker.WebApi.TransferModels;
using BudgetTracker.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static BudgetTracker.Application.Constants.Constants;

namespace BudgetTracker.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDtoConverter _dtoConverter;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionController(
        ITransactionService transactionService,
        UserManager<ApplicationUser> userManager,
        IDtoConverter dtoConverter,
        IUnitOfWork unitOfWork)
    {
        _transactionService = transactionService;
        _userManager = userManager;
        _dtoConverter = dtoConverter;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [Route("transactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionTypeDto>))]
    public async Task<IActionResult> ListTransactionTypes()
    {
        var userId = _userManager.GetUserId(User);
        var transactionTypes = await _transactionService.ListTransactionTypes(userId);
        var transactionTypesDto = _dtoConverter.ConvertToTransactionTypeDto(transactionTypes);

        return Ok(transactionTypesDto);
    }

    [HttpGet]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransactionDto>))]
    public async Task<IActionResult> ListTransactions(string? startDate=null, string? endDate=null)
    {
        var isStartDateValid = DateTime.TryParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal, out var startDateExact);
        var isEndDateValid = DateTime.TryParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal, out var endDateExact);
        startDateExact = startDateExact.SetKindUtc();
        endDateExact = endDateExact.SetKindUtc();

        if (!isStartDateValid || !isEndDateValid)
        {
            return BadRequest("The start date and end date are not valid date times!");
        }

        var userId = _userManager.GetUserId(User);
        var transactions = await _transactionService.ListTransactions(startDateExact, endDateExact, userId);
        var transactionDtos = _dtoConverter.ConvertToTransactionDto(transactions);

        return Ok(transactionDtos);
    }

    [HttpPost]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    public async Task<IActionResult> AddTransactions(IEnumerable<AddTransactionRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        var categoryIds = requests.DistinctBy(x => x.CategoryId).Select(x => x.CategoryId);
        var transactionTypeIds = requests.DistinctBy(x => x.TransactionTypeId).Select(x => x.TransactionTypeId);
        var (categories, transactionTypes) = await _transactionService.GetCategoriesAndTransactionTypes(userId, categoryIds, transactionTypeIds);
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
                throw new CategoryNotFoundException(request.CategoryId);
            }
            if (transactionType == null)
            {
                throw new TransactionTypeNotFoundException(request.TransactionTypeId);
            }

            var transactionId = Guid.NewGuid().ToString();
            transactions.Add(new Transaction(request.TransactionDate, transactionId, request.TransactionAmount, 
                request.Currency, request.Description, transactionType, category, userId));
        }

        await _transactionService.AddTransactions(transactions);

        return Ok(new GenericResponse { HasError = false });
    }

    [HttpPut]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransactions(List<UpdateTransactionRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        var transactions = await _transactionService.GetTransactions(requests.Select(x => x.TransactionId), userId);

        _dtoConverter.UpdateTransactionsDomain(requests, transactions);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteTransactions(IEnumerable<DeleteTransactionRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _transactionService.DeleteTransactions(requests.Select(x => x.TransactionId), userId);

        return Ok();
    }

    [HttpPost]
    [Route("transactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddTransactionTypes(IEnumerable<AddTransactionTypeRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        const bool isDefaultType = false;

        var transactionTypes = _dtoConverter.ConvertToTransactionTypeDomain(requests, isDefaultType, userId);
        await _transactionService.AddTransactionTypes(transactionTypes);

        return Ok();
    }

    [HttpPut]
    [Route("transactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTransactionTypes(List<UpdateTransactionTypeRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        var transactionTypes = await _transactionService.GetTransactionTypes(requests.Select(x => x.TransactionTypeId), userId);

        _dtoConverter.UpdateTransactionTypesDomain(requests, transactionTypes);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("transactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteTransactionTypes(IEnumerable<DeleteTransactionTypeRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _transactionService.DeleteTransactionTypes(requests.Select(x => x.TransactionTypeId), userId);

        return Ok();
    }

    [HttpPost]
    [Route("defaultTransactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddDefaultTransactionTypes(List<AddTransactionTypeRequest> requests)
    {
        const bool isDefaultTransactionType = true;

        var transactionTypes = _dtoConverter.ConvertToTransactionTypeDomain(requests, isDefaultTransactionType);
        await _transactionService.AddTransactionTypes(transactionTypes);

        return Ok();
    }

    [HttpDelete]
    [Route("defaultTransactionTypes")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDefaultTransactionTypes(List<DeleteTransactionTypeRequest> requests)
    {
        var transactionTypes = _userManager.GetUserId(User);
        await _transactionService.DeleteDefaultTransactionTypes(requests.Select(x => x.TransactionTypeId));

        return Ok();
    }
}
