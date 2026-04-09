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
        var products = await context.Products
            .Select(product => new
            {
                product.Id,
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

}