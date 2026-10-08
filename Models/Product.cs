using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("product")]
public class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("product_name")]
    public required string ProductName { get; set; }
    
    [Column("price")]
    public double Price { get; set; }
    
    [Column("category_id")]
    public int CategoryId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
    
}