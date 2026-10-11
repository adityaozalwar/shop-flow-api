using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    // [HttpGet]
    // public async Task<ApiResponse> GetProducts()
    //     => ApiResponse.Ok("Fetched products successfully", await _productService.GetProductsAsync());
    
    // GET : /api/products?pageNumber=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "productId", [FromQuery] string sortOrder = "asc")
    { 
        var result = await _productService.GetPagedProductsAsync(pageNumber, pageSize, sortBy, sortOrder);
        var response = ApiResponse.Custom(200, "Products retrieved successfully.", result);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("{id}")]
    public async Task<ApiResponse> GetProductById([FromRoute] int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return ApiResponse.NotFound("Product not found");
        }

        return ApiResponse.Ok("Fetched product successfully", product);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> CreateProduct([FromBody] ProductDto productDto)
        => ApiResponse.Ok("Product created successfully", await _productService.CreateProductAsync(productDto));

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> UpdateProduct([FromRoute] int id, [FromBody] ProductDto productDto)
    {
        var product = await _productService.UpdateProductAsync(id, productDto);

        if (product == null)
        {
            return ApiResponse.NotFound("Product not found");
        }
        
        return ApiResponse.Ok("Product updated successfully", product);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteProduct([FromRoute] int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);

        if (!deleted)
        {
            return ApiResponse.NotFound("Product not found");
        }

        return ApiResponse.Ok("Product deleted successfully", null);
    }
}