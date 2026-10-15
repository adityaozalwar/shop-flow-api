using ShopFlowAPI.Mappers;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Services;


public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _unitOfWork.Category.GetAllAsync();

        return categories .Select(CategoryMapper.ToDto).ToList();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.Category.GetByIdAsync(id);

        return category == null ? null : CategoryMapper.ToDto(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
    {
        var category = CategoryMapper.ToEntity(categoryDto);

        category.CreatedAt = DateTime.UtcNow;
        category.ModifiedAt = DateTime.UtcNow;

        await _unitOfWork.Category.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return CategoryMapper.ToDto(category);
    }

    public async Task<CategoryDto?> UpdateCategoryAsync( int id, CategoryDto categoryDto)
    {
        var category = CategoryMapper.ToEntity(categoryDto);
        category.ModifiedAt = DateTime.UtcNow;

        var updatedCategory =
            await _unitOfWork.Category.UpdateAsync(id, category);

        if (updatedCategory == null)
            return null;

        await _unitOfWork.SaveChangesAsync();

        return CategoryMapper.ToDto(updatedCategory);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork.Category.GetByIdAsync(id);

        if (category == null)
            return false;

        _unitOfWork.Category.Remove(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
