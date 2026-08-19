using Domain.Entities;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<(List<Product> Products, int TotalCount)> GetAllAsync(
    int pageNumber,
    int pageSize);
    Task<Product?> GetByIdAsync(int id);

    Task<Product> AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);
}