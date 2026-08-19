using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Application.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _service = new ProductService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductExists_ReturnsProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "Ajay",
            CreatedOn = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Laptop", result.ProductName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ReturnsNull()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsProduct()
    {
        // Arrange
        var dto = new CreateProductDto
        {
            ProductName = "Mobile",
            CreatedBy = "Ajay"
        };

        var createdProduct = new Product
        {
            Id = 1,
            ProductName = "Mobile",
            CreatedBy = "Ajay",
            CreatedOn = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(createdProduct);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Mobile", result.ProductName);

        _repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Product>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenProductExists_ReturnsTrue()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            ProductName = "Old Name",
            CreatedBy = "Ajay",
            CreatedOn = DateTime.UtcNow
        };

        var dto = new UpdateProductDto
        {
            ProductName = "New Name",
            ModifiedBy = "Admin"
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        // Act
        var result = await _service.UpdateAsync(1, dto);

        // Assert
        Assert.True(result);
        Assert.Equal("New Name", product.ProductName);
        Assert.Equal("Admin", product.ModifiedBy);

        _repositoryMock.Verify(
            r => r.UpdateAsync(product),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var dto = new UpdateProductDto
        {
            ProductName = "New Name",
            ModifiedBy = "Admin"
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _service.UpdateAsync(999, dto);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            r => r.UpdateAsync(It.IsAny<Product>()),
            Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenProductExists_ReturnsTrue()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "Ajay",
            CreatedOn = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(product);

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        Assert.True(result);

        _repositoryMock.Verify(
            r => r.DeleteAsync(product),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenProductDoesNotExist_ReturnsFalse()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _service.DeleteAsync(999);

        // Assert
        Assert.False(result);

        _repositoryMock.Verify(
            r => r.DeleteAsync(It.IsAny<Product>()),
            Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsProductsAndTotalCount()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                ProductName = "Laptop",
                CreatedBy = "Ajay",
                CreatedOn = DateTime.UtcNow
            },
            new Product
            {
                Id = 2,
                ProductName = "Mobile",
                CreatedBy = "Ajay",
                CreatedOn = DateTime.UtcNow
            }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync(1, 10))
            .ReturnsAsync((products, 2));

        // Act
        var result = await _service.GetAllAsync(1, 10);

        // Assert
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Products.Count);
        Assert.Equal("Laptop", result.Products[0].ProductName);
        Assert.Equal("Mobile", result.Products[1].ProductName);
    }
}