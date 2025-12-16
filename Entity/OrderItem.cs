using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodNet.Entity;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }
    
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }
}