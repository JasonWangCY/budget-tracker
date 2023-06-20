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
public class CategoryController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICategoryService _categoryService;

    public CategoryController(
        UserManager<ApplicationUser> userManager,
        ICategoryService categoryService)
    {
        _userManager = userManager;
        _categoryService = categoryService;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
    public async Task<IActionResult> AddCategories(List<AddCategoryRequest> request)
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
}
