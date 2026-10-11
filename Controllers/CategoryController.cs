using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET : /api/categories
    [HttpGet]
    public async Task<ApiResponse> GetCategories()
        => ApiResponse.Ok("Fetched categories successfully", await _categoryService.GetCategoriesAsync());

    // GET : /api/categories/{id}
    [HttpGet("{id}")]
    public async Task<ApiResponse> GetCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return ApiResponse.NotFound("Category not found");
        }

        return ApiResponse.Ok("Fetched category successfully", category);
    }

    //POST : /api/categories
    [HttpPost]
    public async Task<ApiResponse> CreateCategory([FromBody] CategoryDto categoryDto)
        => ApiResponse.Ok("Category created successfully", await _categoryService.CreateCategoryAsync(categoryDto));

    // PUT : /api/categories/{id}
    [HttpPut("{id}")]
    public async Task<ApiResponse> UpdateCategory( [FromRoute] int id, [FromBody] CategoryDto categoryDto)
    {
        var category = await _categoryService.UpdateCategoryAsync(id, categoryDto);

        if (category == null)
        {
            return ApiResponse.NotFound("Category not found");
        }

        return ApiResponse.Ok("Category updated successfully", category);
    }

    // DELETE : /api/categories/{id}
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteCategory([FromRoute] int id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);

        if (!deleted)
        {
            return ApiResponse.NotFound("Category not found");
        }

        return ApiResponse.Ok("Category deleted successfully", null);
    }
}