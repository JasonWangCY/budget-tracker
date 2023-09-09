using BudgetTracker.Domain.PersistenceInterfaces;
using BudgetTracker.Domain.Services.Interfaces;
using BudgetTracker.Infrastructure.Identity;
using BudgetTracker.WebApi.Services.Interfaces;
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
public class CategoryController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICategoryService _categoryService;
    private readonly IDtoConverter _dtoConverter;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(
        UserManager<ApplicationUser> userManager,
        ICategoryService categoryService,
        IDtoConverter dtoConverter,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _categoryService = categoryService;
        _dtoConverter = dtoConverter;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
    public async Task<IActionResult> ListCategories()
    {
        var userId = _userManager.GetUserId(User);
        var categories = await _categoryService.ListCategories(userId);
        var categoriesDto = categories.Select(x => new CategoryDto
        {
            CategoryId = x.CategoryId,
            CategoryName = x.CategoryName,
            Description = x.Description,
            IsDefaultCategory = x.IsDefaultCategory
        });

        return Ok(categoriesDto);
    }

    [HttpPost]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddCategories(List<AddCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        const bool isDefaultCategory = false;

        var categories = _dtoConverter.ConvertToCategoryDomain(requests, isDefaultCategory, userId);
        await _categoryService.AddCategories(categories);

        return Ok();
    }

    // TODO: We need concurrency control to prevent race condition here.
    // Let's use optimistic concurrency with versioning in Postgres
    [HttpPut]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategories(List<UpdateCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);

        var categories = await _categoryService.GetCategories(requests.Select(x => x.CategoryId), userId);
        _dtoConverter.UpdateCategoriesDomain(requests, categories);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCategories(List<DeleteCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _categoryService.DeleteCategories(requests.Select(x => x.CategoryId), userId);

        return Ok();
    }

    [HttpPost]
    [Route("default")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddDefaultCategories(List<AddCategoryRequest> requests)
    {
        const bool isDefaultCategory = true;

        var categories = _dtoConverter.ConvertToCategoryDomain(requests, isDefaultCategory);
        await _categoryService.AddCategories(categories);

        return Ok();
    }

    [HttpDelete]
    [Route("default")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDefaultCategories(List<DeleteCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _categoryService.DeleteDefaultCategories(requests.Select(x => x.CategoryId));

        return Ok();
    }
}
