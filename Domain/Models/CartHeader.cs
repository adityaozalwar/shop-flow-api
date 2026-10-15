using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("cart_header")]
public class CartHeader
{
    [Key]
    [Column("cart_header_id")]
    public int CartHeaderId { get; set; }
    
    [Column("user_id")]
    public required string UserId { get; set; }
    
    [Column("cart_price")]
    public double CartPrice { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
    
    public Order? Order { get; set; }

    public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
}