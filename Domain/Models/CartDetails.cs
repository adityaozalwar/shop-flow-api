using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("cart_details")]
public class CartDetails
{
    [Key]
    [Column("cart_details_id")]
    public int CartDetailsId { get; set; }
    
    [Column("cart_header_id")]
    public int CartHeaderId { get; set; }
    
    [ForeignKey(nameof(CartHeaderId))]
    public CartHeader CartHeader { get; set; } = null!;
    
    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
    
    [Column("cart_quantity")]
    public int CartQuantity { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
}