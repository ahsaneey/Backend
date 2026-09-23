using Calligraphy.Application.DTOs.Common;
using Calligraphy.Application.Interfaces.Services;
using Calligraphy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(
            ApiResponse<IEnumerable<Product>>.Ok(
                products,
                "Products retrieved successfully."
            )
        );
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound(
                ApiResponse<Product>.Fail("Product not found.")
            );
        }

        return Ok(
            ApiResponse<Product>.Ok(
                product,
                "Product retrieved successfully."
            )
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var createdProduct = await _productService.CreateAsync(product);

        return Ok(
            ApiResponse<Product>.Ok(
                createdProduct,
                "Product created successfully."
            )
        );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest(
                ApiResponse<Product>.Fail(
                    "Product ID does not match."
                )
            );
        }

        var updatedProduct = await _productService.UpdateAsync(product);

        if (updatedProduct == null)
        {
            return NotFound(
                ApiResponse<Product>.Fail(
                    "Product not found."
                )
            );
        }

        return Ok(
            ApiResponse<Product>.Ok(
                updatedProduct,
                "Product updated successfully."
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                ApiResponse<object>.Fail(
                    "Product not found."
                )
            );
        }

        return Ok(
            ApiResponse<object>.Ok(
                null,
                "Product deleted successfully."
            )
        );
    }
}