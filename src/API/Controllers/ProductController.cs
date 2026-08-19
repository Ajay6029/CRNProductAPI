using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/v1/Product
    [HttpGet]
   [HttpGet]
public async Task<IActionResult> GetAll(
    int pageNumber = 1,
    int pageSize = 10)
{
    if (pageNumber < 1)
    {
        return BadRequest(new
        {
            message = "Page number must be greater than 0."
        });
    }

    if (pageSize < 1 || pageSize > 100)
    {
        return BadRequest(new
        {
            message = "Page size must be between 1 and 100."
        });
    }

    var result = await _productService.GetAllAsync(
        pageNumber,
        pageSize);

    var totalPages = (int)Math.Ceiling(
        result.TotalCount / (double)pageSize);

    return Ok(new
    {
        pageNumber,
        pageSize,
        totalCount = result.TotalCount,
        totalPages,
        items = result.Products
    });
}

    // GET: api/v1/Product/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(new
            {
                message = $"Product with ID {id} was not found."
            });
        }

        return Ok(product);
    }

    // POST: api/v1/Product
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    // PUT: api/v1/Product/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductDto dto)
    {
        var updated = await _productService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = $"Product with ID {id} was not found."
            });
        }

        return NoContent();
    }

    // DELETE: api/v1/Product/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Product with ID {id} was not found."
            });
        }

        return NoContent();
    }
}