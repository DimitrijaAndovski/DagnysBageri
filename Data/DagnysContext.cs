using DagnysBageri.Entities;
using Microsoft.EntityFrameworkCore;

namespace DagnysBageri.Data;

public class DagnysContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}