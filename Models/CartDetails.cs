using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("cart_details")]
public class CartDetails
{
    [Key]
    [Column("cart_details_id")]
    public int CartDetailsId { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    [Column("cart_quantity")]
    public int CartQuantity { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
}