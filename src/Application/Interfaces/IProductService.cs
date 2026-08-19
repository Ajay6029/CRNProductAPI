using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<(List<ProductDto> Products, int TotalCount)> GetAllAsync(
        int pageNumber,
        int pageSize);

    Task<ProductDto?> GetByIdAsync(int id);

    Task<ProductDto> CreateAsync(CreateProductDto dto);

    Task<bool> UpdateAsync(int id, UpdateProductDto dto);

    Task<bool> DeleteAsync(int id);
}