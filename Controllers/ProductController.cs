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
    
    [HttpGet]
    public async Task<ApiResponse> GetProducts()
        => ApiResponse.Ok("Fetched products successfully", await _productService.GetProductsAsync());

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
    public async Task<ApiResponse> CreateProduct([FromBody] ProductDto productDto)
        => ApiResponse.Ok("Product created successfully", await _productService.CreateProductAsync(productDto));

    [HttpPut("{id}")]
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