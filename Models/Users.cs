using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopFlowAPI.Enums;

namespace ShopFlowAPI.Models;

[Table("users")]
public class Users
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("user_name")]
    public required string UserName { get; set; }
    
    [Column("password")]
    public required string Password { get; set; }
    
    [Column("email")]
    public required string Email { get; set; }
    
    [Column("role")]
    public Role Role { get; set; }
}