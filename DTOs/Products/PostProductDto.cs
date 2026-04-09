namespace DagnysBageri.DTOs;

public record PostProductDto
{
    public int SupplierId { get; set; }
    public int Id { get; set; }
    public string ProductNumber { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; } 
}