using System.Diagnostics.CodeAnalysis;

namespace DagnysBageri.Entities;

public record Product
{
    public int Id { get; set; }
    [NotNull]
    public string ProductNumber { get; set; }
    [NotNull]
    public string ProductName { get; set; }
    [NotNull]
    public decimal Price { get; set; } 
    public Supplier Supplier { get; set; }
}