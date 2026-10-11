using ShopFlowAPI.Mappers;
using ShopFlowAPI.Models.DTOs;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services.IServices;

namespace ShopFlowAPI.Services;

public class ProductService : IProductService
{
    
    private readonly IUnitOfWork _unitOfWork ;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var products = await _unitOfWork.Product.GetAllAsync();
        return products.Select(ProductMapper.ToDto).ToList();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(id);
        return product == null ? null : ProductMapper.ToDto(product);
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        var product = ProductMapper.ToEntity(productDto);
        product.CreatedAt = DateTime.UtcNow;
        product.ModifiedAt = DateTime.UtcNow;

        await _unitOfWork.Product.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return ProductMapper.ToDto(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto)
    {
        var product = ProductMapper.ToEntity(productDto);
        product.ModifiedAt = DateTime.UtcNow;

        var updatedProduct = await _unitOfWork.Product.UpdateAsync(id, product);
        if (updatedProduct == null) return null;

        await _unitOfWork.SaveChangesAsync();
        return ProductMapper.ToDto(updatedProduct);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(id);
        if (product == null) return false;

        _unitOfWork.Product.Remove(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<PagedResponse<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize, string sortBy, string sortOrder)
    {
        if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be at least 1.");
        if (pageSize < 1 || pageSize > 100) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be between 1 and 100.");
        if (string.IsNullOrWhiteSpace(sortOrder)) sortOrder = "asc";
        if (!sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase) && !sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("sortOrder must be 'asc' or 'desc'.");

        var result = await _unitOfWork.Product.GetPagedProductsAsync(pageNumber, pageSize, sortBy, sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase));
        var items = result.Items.Select(ProductMapper.ToDto).ToList();

        return new PagedResponse<ProductDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = result.TotalCount,
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize)
        };
    }
}