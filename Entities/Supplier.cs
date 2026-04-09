
using System.Diagnostics.CodeAnalysis;

namespace DagnysBageri.Entities;

public record Supplier
{
    public int SupplierId { get; set; }
    [NotNull]
    public string SupplierName { get; set; }
    public string ContactPerson { get; set; }  
    public string Address { get; set; }
    [NotNull]
    public string Phone { get; set; }
    [NotNull]
    public string Email { get; set; }
    public List<Product> Products { get; set; }
}
