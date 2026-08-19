using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

   public async Task<(List<ProductDto> Products, int TotalCount)> GetAllAsync(
    int pageNumber,
    int pageSize)
{
    var result = await _repository.GetAllAsync(
        pageNumber,
        pageSize);

    var products = result.Products
        .Select(MapToDto)
        .ToList();

    return (products, result.TotalCount);
}

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            ProductName = dto.ProductName,
            CreatedBy = dto.CreatedBy,
            CreatedOn = DateTime.UtcNow
        };

        var createdProduct = await _repository.AddAsync(product);

        return MapToDto(createdProduct);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        product.ProductName = dto.ProductName;
        product.ModifiedBy = dto.ModifiedBy;
        product.ModifiedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        await _repository.DeleteAsync(product);

        return true;
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedBy = product.CreatedBy,
            CreatedOn = product.CreatedOn,
            ModifiedBy = product.ModifiedBy,
            ModifiedOn = product.ModifiedOn
        };
    }
}