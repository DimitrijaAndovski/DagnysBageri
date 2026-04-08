using DagnysBageri.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data;

public class DagnysContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}