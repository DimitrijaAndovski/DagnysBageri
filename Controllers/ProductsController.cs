using DagnysBageri.DTOs;
using DagnysBageri.Data;
using DagnysBageri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageri.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(DagnysContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        // Projecering, bara returnera det som behövs i frontend...
        var products = await context.Products
            .Select(product => new
            {
                product.Id,
                product.ProductName,
                product.Price
            })
            .ToListAsync();

        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Items = products.Count,
            Data = products
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
            .Where(c => c.Id == id)
            .Select(p => new
            {
                p.ProductNumber,
                p.Price,
                p.ProductName,
            })
            .SingleOrDefaultAsync();
        if (product is not null)
        {
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }

        return NotFound();
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto product)
    {
        Supplier supplier = await context.Suppliers.FindAsync(product.SupplierId);
        if (supplier is null) return NotFound();

        Product item = new()
        {
            ProductNumber = product.ProductNumber,
            ProductName = product.ProductName,
            Price = product.Price,
            Supplier = supplier
        };

        context.Products.Add(item);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(FindProduct), new { id = item.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        Product productToUpdate = await context.Products.FindAsync(id);
        if (productToUpdate is null) return NotFound();

        productToUpdate.ProductNumber = product.ProductNumber;
        productToUpdate.Price = product.Price;
        productToUpdate.ProductName = product.ProductName;

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        Product product = await context.Products.FindAsync(id);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchProduct(int id, Product product)
    {
        Product productToPatch = await context.Products.FindAsync(id);
        if (productToPatch is null) return NotFound();

        productToPatch.Price = product.Price;

        await context.SaveChangesAsync();
        return NoContent();
    }
}