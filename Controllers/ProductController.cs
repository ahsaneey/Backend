<<<<<<< HEAD
﻿using Calligraphy.Application.Interfaces.Services;
=======
﻿using Calligraphy.Application.Interfaces.Repositories;
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
using Calligraphy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
<<<<<<< HEAD
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
=======
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
<<<<<<< HEAD
        var products = await _productService.GetAllAsync();
=======
        var products = await _productRepository.GetAllAsync();
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
<<<<<<< HEAD
        var product = await _productService.GetByIdAsync(id);
=======
        var product = await _productRepository.GetByIdAsync(id);
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

<<<<<<< HEAD
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var createdProduct = await _productService.CreateAsync(product);

        return Ok(createdProduct);
=======
   
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return Ok(product);
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
<<<<<<< HEAD
        if (id != product.Id)
        {
            return BadRequest("Product ID does not match.");
        }

        var updatedProduct = await _productService.UpdateAsync(product);

        if (updatedProduct == null)
=======
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null)
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
        {
            return NotFound();
        }

<<<<<<< HEAD
        return Ok(updatedProduct);
=======
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.ImageUrl = product.ImageUrl;
        existingProduct.StockQuantity = product.StockQuantity;

        await _productRepository.UpdateAsync(existingProduct);
        await _productRepository.SaveChangesAsync();

        return Ok(existingProduct);
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
<<<<<<< HEAD
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
=======
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
        {
            return NotFound();
        }

<<<<<<< HEAD
=======
        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

>>>>>>> 99b30f21cfc0eb191daf1a64225c8f3cd3d30208
        return NoContent();
    }
}