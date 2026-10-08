using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopFlowAPI.Enums;

namespace ShopFlowAPI.Models;

[Table("order")]
public class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }
    
    [Column("cart_header_id")]
    public int CartHeaderId { get; set; }
    
    [Column("order_status")]
    public OrderStatus OrderStatus { get; set; }
    
    [Column("payment_status")]
    public PaymentStatus PaymentStatus { get; set; }
    
    [Column("order_date")]
    public DateTime OrderDate { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
}