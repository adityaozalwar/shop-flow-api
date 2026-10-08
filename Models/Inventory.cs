using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("inventory")]
public class Inventory
{
    [Key]
    [Column("inventory_id")]
    public int InventoryId { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("reserved_quantity")]
    public int ReservedQuantity { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
}
