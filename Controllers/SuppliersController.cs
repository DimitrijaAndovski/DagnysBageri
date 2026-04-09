using DagnysBageri.Data;
using DagnysBageri.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(DagnysContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        List<Supplier> suppliers = await context.Suppliers.ToListAsync();
        return Ok(new { Success = true, StatusCode = 200, Items = suppliers.Count, Data = suppliers });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        Supplier supplier = await context.Suppliers
            .Include(s => s.Products)
            .SingleOrDefaultAsync(s => s.SupplierId == id);
        if (supplier is null) return NotFound();

        var supplierToReturn = new
        {
            supplier.SupplierId,
            supplier.SupplierName,
            Products = supplier.Products.Select(p => new
            {
                p.ProductNumber,
                p.ProductName,
                p.Price
            })

        };
        return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = supplierToReturn });
    }

    [HttpPost()]
    public async Task<ActionResult> AddSupplier(Supplier supplier)
    {
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(FindSupplier), new { id = supplier.SupplierId }, supplier);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveSupplier(int id)
    {
        Supplier supplier = await context.Suppliers.FindAsync(id);
        if (supplier is not null)
        {
            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();
        }

        return NoContent();
    }

}