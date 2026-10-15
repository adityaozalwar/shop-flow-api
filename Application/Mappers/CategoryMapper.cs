using ShopFlowAPI.Models;
using ShopFlowAPI.Models.DTOs;

namespace ShopFlowAPI.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToDto(Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        };
    }

    public static Category ToEntity(CategoryDto categoryDto)
    {
        return new Category
        {
            CategoryId = categoryDto.CategoryId,
            CategoryName = categoryDto.CategoryName
        };
    }
}
