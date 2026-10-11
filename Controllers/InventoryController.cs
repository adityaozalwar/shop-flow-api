using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    
    // GET : /api/inventory
    [HttpGet]
    public async Task<ApiResponse> GetInventories()
        => ApiResponse.Ok("Fetched inventory successfully", await _inventoryService.GetInventoriesAsync());

    
    // GET :/api/inventory/{id}
    [HttpGet("{id}")]
    public async Task<ApiResponse> GetInventoryById([FromRoute] int id)
    {
        var inventory = await _inventoryService.GetInventoryByIdAsync(id);

        if (inventory == null)
            return ApiResponse.NotFound("Inventory not found");

        return ApiResponse.Ok("Fetched inventory successfully", inventory);
    }

    
    // POST : /api/inventory
    [HttpPost]
    public async Task<ApiResponse> CreateInventory( [FromBody] InventoryDto inventoryDto)
        => ApiResponse.Ok("Inventory created successfully", await _inventoryService.CreateInventoryAsync(inventoryDto));

    // PUT : /api/inventory/{id}
    [HttpPut("{id}")]
    public async Task<ApiResponse> UpdateInventory( [FromRoute] int id, [FromBody] InventoryDto inventoryDto)
    {
        var inventory =
            await _inventoryService.UpdateInventoryAsync(id, inventoryDto);

        if (inventory == null)
            return ApiResponse.NotFound("Inventory not found");

        return ApiResponse.Ok("Inventory updated successfully", inventory);
    }

    // DELETE : /api/inventory/{id} 
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteInventory([FromRoute] int id)
    {
        var deleted = await _inventoryService.DeleteInventoryAsync(id);

        if (!deleted)
            return ApiResponse.NotFound("Inventory not found");

        return ApiResponse.Ok("Inventory deleted successfully", null);
    }
}
