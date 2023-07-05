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

    public CategoryController(
        UserManager<ApplicationUser> userManager,
        ICategoryService categoryService,
        IDtoConverter dtoConverter)
    {
        _userManager = userManager;
        _categoryService = categoryService;
        _dtoConverter = dtoConverter;
    }

    [HttpGet]
    [Route("listCategories")]
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
    [Route("addCategories")]
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

    [HttpPost]
    [Route("deleteCategories")]
    [AuthorizeRoles(UserRole.ADMIN, UserRole.USER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCategories(List<DeleteCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _categoryService.DeleteCategories(requests.Select(x => x.CategoryId), userId);

        return Ok();
    }

    [HttpPost]
    [Route("addDefaultCategories")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddDefaultCategories(List<AddCategoryRequest> requests)
    {
        const bool isDefaultCategory = true;

        var categories = _dtoConverter.ConvertToCategoryDomain(requests, isDefaultCategory);
        await _categoryService.AddCategories(categories);

        return Ok();
    }

    [HttpPost]
    [Route("deleteDefaultCategories")]
    [AuthorizeRoles(UserRole.ADMIN)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDefaultCategories(List<DeleteCategoryRequest> requests)
    {
        var userId = _userManager.GetUserId(User);
        await _categoryService.DeleteDefaultCategories(requests.Select(x => x.CategoryId));

        return Ok();
    }
}
